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

mt() {
	echo "$1 > $2" | bc
} 

sort() {

	local l=${#chances[@]}
	for (( i = 0; i < l; i++ )); do
		for (( j = 0; j < (l - i - 1); j++ )); do
			if [[ $(mt ${chances[$j]} ${chances[$j+1]}) == 1 ]]; then
				swap $j $(echo "$j + 1" | bc)
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

make() {
	local bit=$1
	local name=$2
	for (( i = 0; i < ${#name}; i = i + size )); do
		symbol=${name:$i:$size}
		codes[${symbol}]=$(echo "${bit}${codes[$symbol]}")
	done
}

encodeGrams() {
	tmpChances=("${chances[@]}")
	tmpSymbols=("${symbols[@]}")

	while [[ ${#symbols[@]} -ne 1 ]]; do
		make 1 ${symbols[0]}
		make 0 ${symbols[1]}

		merge="${symbols[1]}${symbols[0]}"
		chance=$(echo "${chances[0]} + ${chances[1]}" | bc)

		unset symbols[0]
		symbols=("${symbols[@]}")
		unset chances[0]
		chances=("${chances[@]}")
		unset symbols[0]
		symbols=("${symbols[@]}")
		unset chances[0]
		chances=("${chances[@]}")

		symbols=(${merge} ${symbols[@]})
		chances=(${chance} ${chances[@]})
		sort
	done

	chances=("${tmpChances[@]}")
	symbols=("${tmpSymbols[@]}")
}

printCodes() {
	local tmp
	for (( i = ${#symbols[@]} - 1; i >= 0; i-- )); do
		tmp+=(${symbols[$i]})
	done

	echo -e "${out}N-grams:${_out}"
	for (( i = 0; i < ${#tmp[@]}; i++ )); do
		symbol=${tmp[$i]}
		echo -e "    ${symbol} is ${codes[$symbol]}"
	done
}

encodeMessage() {
	local code
	for (( i = 0; i < ${#message}; i = i + ${size} )); do
		tmp=${message:$i:$size}
		code+=${codes[$tmp]}
	done

	echo $code
}

echo -e "${inp}Input array of symbols:${_inp}"
read -a symbols

for (( i = ${#symbols[@]} - 1; i >= 0; i-- )); do
	tmp+=(${symbols[$i]})
done
symbols=(${tmp[@]})
unset tmp

echo -e "${inp}Input array of chances:${_inp}"
read -a chances
for (( i = ${#chances[@]} - 1; i >= 0; i-- )); do
	tmp+=(${chances[$i]})
done
chances=(${tmp[@]})
unset tmp

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
declare -A codes
encodeGrams
printCodes
echo ""

h=0
for (( i = 0; i < ${#chances[@]}; i++ )); do
	h=$(echo "scale=${scale}; ${h} + ${chances[$i]} * l(${chances[$i]})/l(2)" | bc -l)
done
h=$(echo "scale=${scale}; ${h}/-${size}" | bc)
echo "H = $h"

l=0
for (( i = 0; i < ${#chances[@]}; i++ )); do
	symbol=${symbols[$i]}
	l=$(echo "$l + ${#codes[$symbol]} * ${chances[$i]}" | bc)
done
l=$(echo "scale=${scale}; ${l}/${size}" | bc)
echo "L = $l"

printf "${inp}It it needed to encode a message? [y/N]: ${_inp}"
read yesno

if [[ $yesno =~ ^(y|yes|д|да)$ ]]; then
	echo -e "${inp}Input a message:${_inp}"
	read message
	code=$(encodeMessage)
	echo -e "${out}Coded message:${_out} $code"
fi