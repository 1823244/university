contains() {
	local e
	i=0
	for e in "${@:2}"; do 
		if [[ "$e" == "$1" ]]; then
			echo $i
			return
		fi
		let i++
	done
	echo -1
}

findSpecial() {
	eval "var=(\${condition$1[@]})"
	for (( j = 0; j < ${#var}; j++ )); do
		if [[ special -lt ${var[$j]} ]]; then
			special=${var[$j]}
		fi
	done
}

getCode() {
	local var=regex0
	local j=0
	while [[ ${#var} -ne 0 ]]; do
		if [[ $1 =~ $var ]]; then
			echo $j
			return
		fi
		let j++
		eval "var=\$regex$j"
	done

	echo 0
}

output() {
	name=$1
	value=$1
	type=$2

	if [[ (${name:0:1} == "'") || (${name:0:1} == "\"") ]]; then
		OIFS=$IFS
		IFS=$'\n'
		local arr=( $( echo "$name" | sed -e 's/+/\n/g' ) )
		for (( i = 0; i < ${#arr}; i++ )); do
			arr[$i]=${arr[$i]:1:${#arr[$i]}-2}
			arr[$i]=${arr[$i]//\'/\'\'}
			result+=${arr[$i]}
		done
		value="'${result}'"
		IFS=$OIFS
		type=3
	elif [[ ${name:0:1} =~ [0-9] ]]; then
		type=2
	elif [[ ${name:0:1} = [a-zA-Z] ]]; then
		if [[ keyword=$(contains ${name} ${keywords[@]}) -ne -1 ]]; then
			let type=4+$keyword 
		else 
			type=1
		fi
	elif [[ ${name} == "+" ]]; then
	 	type=15
	elif [[ ${name} == "-" ]]; then
	 	type=16
	elif [[ $(contains ${name} ${operations[@]}) -ne -1 ]]; then
	 	type=17
	elif [[ mark=$(contains ${name} ${marks[@]}) -ne -1 ]]; then
		let type=18+$mark
	fi

	printf "%-20s%-20s%-20s\n" "$name" "$value" "$type"
	# o=$name
}

analysis() {
	declare -A lexeme=([name]="" [type]=-1)
	local s=0
	local index=0

	line+=#
	local current=${line:index:1}

	while [[ index -lt ${#line} ]]; do
		lexeme=([name]="" [type]=-1)
		while [[ (s -ge 0) && (index -lt ${#line}) ]]; do
			while [[ $(getCode $current) -eq 0 ]]; do
				let index++
				[[ index -ge ${#line} ]] && return
				current=${line:index:1}
			done

			i=$(getCode $current)
			eval "s=\${condition$i[$s]}"
			[[ (s -lt 0) || (s -eq ${special}) ]] && break
			lexeme[name]+=$current
			if [[ index -eq $(( ${#line} - 1 )) ]]; then
				current=${line:index:1}
				let index++
			else
				let index++
				current=${line:index:1}
			fi
		done

		if [[ $(contains $s ${accept[@]}) -ne -1 ]]; then
			echo $(output ${lexeme[@]})
			# o=${line}
			# echo 1
			s=0
			if [[ ${current} == "#" ]]; then
				return
			fi
		else
			case "$s" in
				"-1" ) error="Строка $n. Ошибка.";;
				"-2" ) error="Строка $n. После : ожидался знак =.";;
				"-3" ) error="Строка $n. Часть строковой константы должна начинаться с ' или \".";;
				"-4" ) error="Строка $n. Незавершённая часть константы.";;
			esac
			return
		fi
	done
}

i=0
special=0
while read row; do
	eval "condition$i=($row)"
	findSpecial $i
	let i++
done < "conditions"
# n=$i

i=0
while read row; do
	eval "regex$i=\"$row\""
	let i++
done < "regexs"

read row < "accept"
accept=($row)

read row < "keywords"
keywords=($row)

read row < "operations"
operations=($row)

read row < "marks"
marks=($row)

n=0
o=0
while read line; do
	let n++
	error=
	analysis
	# echo ${o}
	if [[ -n "$error" ]]; then
		echo "$error"
	fi
done < "input"