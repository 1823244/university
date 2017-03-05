# 'a112#+'+'#12'

s0() {
	if [[ $char == \' ]]; then
		goto 1
	else
		echo "Отвергнуть: константа должна начинаться и заканчиваться символом '."
	fi	
}

s1() {
	if [[ $char == \' ]]; then
		goto 3
	elif [[ $char == \# ]]; then
		goto 4
	else
		goto 2
	fi
}

s2() {
	if [[ $char == \' ]]; then
		goto 3
	else
		goto 2
	fi
}

s3() {
	if [[ $char =~ \' ]]; then
		goto 2
	elif [[ $char == \+ ]]; then
		goto 0
	elif [[ index -eq ${#string} ]]; then
		echo "Допустить."
	else
		echo "Отвергнуть: константы должны соединяться символом +."
	fi
}

s4() {
	if [[ $char =~ [0-7] ]]; then
		goto 5
	else
		echo "Отвергнуть."
	fi
}

s5() {
	if [[ $char =~ \' ]]; then
		goto 8
	elif [[ $char =~ [0-7] ]]; then
		goto 6
	else
		echo "Отвергнуть: код символа может содержать только цифры."
	fi
}

s6() {
	if [[ $char =~ \' ]]; then
		goto 8
	elif [[ $char =~ [0-7] ]]; then
		goto 7
	else
		echo "Отвергнуть: код символа может содержать только цифры."
	fi
}

s7() {
	if [[ $char =~ \' ]]; then
		goto 8
	else
		echo "Отвергнуть: константа должна заканчиваться символом '."
	fi
}

s8() {
	if [[ $char == \+ ]]; then
		goto 0
	elif [[ index -eq ${#string} ]]; then
		echo "Допустить."
	else
		echo "Отвергнуть: константы должны соединяться символом +."
	fi
}

goto() {
	let index++
	if [[ index -eq ${#string}+1 ]]; then
		echo "Отвергнуть."
		return
	fi

	char=${string:index:1}
	$(echo "s$1")
}

compile() {
	let index=-1
	goto 0
}

interpretation() {
	accept=(3 8)
	let i=0
	while read line; do
		eval "sym$i=($line)"
		let i++
	done < "lines"
	n=$i

	let i=0
	while read line; do
		eval "regex$i=\"$line\""
		let i++
	done < "regexs"

	s=0
	index=0
	while [[ s -ge 0 && index -ne ${#string} ]]; do
		l=-1
		for (( i = 0; i < $n; i++ )); do
			eval "var=\$regex$i"
			if [[ ${string:index:1} = $var ]]; then
				l=$i
				break
			fi
		done
		eval "s=\${sym$l[$s]}"
		let index++
	done

	if [[ ${#string} -eq 0 ]]; then
		echo "Допустить."
		return
	fi

	for (( i = 0; i < ${#accept[@]}; i++ )); do
		if [[ s -eq ${accept[$i]} ]]; then
			echo "Допустить."
			return	
		fi
	done

	echo "Отвергнуть."
}

printf "Строка: "; read string
# compile
interpretation