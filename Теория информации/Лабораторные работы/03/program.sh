#267
#adadbdcbbdcabfdafceb

#define section

shopt -s nocasematch
scale=5
err="\e[1;31m"
_err="\e[0m"
war="\e[1;33m"
_war="\e[0m"
inp="\e[1;34m"
_inp="\e[0m"
out="\e[1;32m"
_out="\e[0m"

#/define section

createGrams() {
	declare -a tmpSymbols
	declare -a tmpChances
	length=${#symbols[@]}

	from=$length
	to=$(echo "${length}^${size}" | bc)
	for (( i = 0; i < $to; i++ )); do
		tmpChances[$i]=1
	done

	let to/=$length
	for (( i = 0; i < size; i++ )); do
		index=0
		for (( j = 0; j < from; j++ )); do
			let this=($j % $length)
			for (( k = 0; k < $to; k++ )); do
				tmpSymbols[$index]+=${symbols[$this]}
				tmpChances[$index]=$(echo "${tmpChances[$index]} * ${chances[$this]}" | bc -l)
				let index++
			done
		done
		let from*=$length
		let to/=$length
	done

	symbols=(${tmpSymbols[@]})
	chances=(${tmpChances[@]})
}

swap() {
	tmpC=${chances[$1]}
	tmpS=${symbols[$1]}
	chances[$1]=${chances[$2]}
	symbols[$1]=${symbols[$2]}
	chances[$2]=${tmpC}
	symbols[$2]=${tmpS}
}

sort() {

	local l=${#chances[@]}
	for (( i = 0; i < l; i++ )); do
		for (( j = 0; j < (l - i - 1); j++ )); do
			if [[ ${chances[$j]} < ${chances[$j+1]} ]]; then
				swap $j $(echo "$j + 1" | bc)
				f=1
			fi
		done
	done
}

printGrams() {
	echo -e "${out}N-grams:${_out}"
	for (( i = 0; i < ${#symbols[@]}; i++ )); do
		echo -e "    p(${symbols[$i]}) = ${chances[$i]}"
	done
}

diff() {
	echo "	define abs(i) { 
				if (i < 0) return (-i)
    			return (i)
			}
			abs($1 - $2)" | bc -l
}

le() {
	echo "$1 <= $2" | bc
} 

cut() {
	local l=$1
	local r=$2

	sumL=0
	for (( i = l; i < r; i++ )); do
		sumL=$(echo "${sumL} + ${chances[$i]}" | bc)
	done
	sumR=${chances[$r]}

	local i=$r
	local min=$sumL
	local m=$(diff $sumL $sumR)
	while [[ $(le $m $min) == 1 ]]; do
		let i-=1
		min=$m
		sumL=$(echo "${sumL} - ${chances[$i]}" | bc)
		sumR=$(echo "${sumR} + ${chances[$i]}" | bc)
		m=$(diff $sumL $sumR)
	done

	echo $i
}

fano() {
	local l=$1
	local r=$2
	if [[ $l -lt $r ]]; then
		n=$(cut $l $r)
		for (( i = l; i <= n; i++ )); do
			codes[$i]+=0
		done

		for (( i = n + 1; i <= r; i++ )); do
			codes[$i]+=1
		done

		fano $l $n
		let n+=1
		fano $n $r
	fi
}

encodeGrams() {
	local l=0
	local r=$(echo "${#chances[@]} - 1" | bc)

	fano $l $r 
}

printCodes() {
	echo -e "${out}N-grams:${_out}"
	for (( i = 0; i < ${#symbols[@]}; i++ )); do
		echo -e "    ${symbols[$i]} is ${codes[$i]}"
	done
}

encodeMessage() {
	declare -A symbolToCode
	for (( i = 0; i < ${#symbols[@]}; i++ )); do
		symbolToCode[${symbols[$i]}]=${codes[$i]}
	done

	local code
	for (( i = 0; i < ${#message}; i = i + ${size} )); do
		tmp=${message:$i:$size}
		code+=${symbolToCode[$tmp]}
	done

	echo $code
}

echo -e "${inp}Input array of symbols:${_inp}"
read -a symbols

echo -e "${inp}Input array of chances:${_inp}"
read -a chances

if [[ ${#symbols[@]} -ne ${#chances[@]} ]]; then
	echo -e "${err}Error. Lengths of arrays are not equal.${_err}"
	exit
fi

printf "${inp}Input size of block: ${_inp}"
read size
while [[ !($size =~ ^[0-9]+$) ]]; do
	printf "${war}Invalid data. Input correct size of block value: ${_war}"
	read size
done

if [[ $size -gt 1 ]]; then
	createGrams
fi

sort
# printGrams
declare -a codes
encodeGrams

h=0
for (( i = 0; i < ${#chances[@]}; i++ )); do
	h=$(echo "scale=${scale}; ${h} + ${chances[$i]} * l(${chances[$i]})/l(2)" | bc -l)
done
h=$(echo "scale=${scale}; ${h}/-${size}" | bc)
echo "H = $h"

l=0
for (( i = 0; i < ${#chances[@]}; i++ )); do
	# printf "${#codes[$i]} * ${chances[$i]} + "
	l=$(echo "$l + ${#codes[$i]} * ${chances[$i]}" | bc)
done
l=$(echo "scale=${scale}; ${l}/${size}" | bc)
echo "L = $l"

printCodes

printf "${inp}It it needed to encode a message? [y/N]: ${_inp}"
read yesno

if [[ $yesno =~ ^(y|yes|д|да)$ ]]; then
	echo -e "${inp}Input a message:${_inp}"
	read message
	code=$(encodeMessage)
	echo -e "${out}Coded message:${_out} $code"
fi