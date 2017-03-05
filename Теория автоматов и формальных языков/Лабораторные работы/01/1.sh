input() {
	echo "Введите КС-грамматику:"
	printf "  "; read rule
	i=1
	while [[ ${rule} != 0 ]]; do
		gram[${i}]=${rule}
		let i++
		printf "  "; read rule
	done
}

find() {
	echo ${chain} | grep -bo '[A-Z]'  | head -1 | sed 's/^.*://'
}

replace() {
	e=$(find)
	while [[ -n ${e} ]]; do
		echo "Терминальная цепочка: ${chain}"
		echo "Можно применить:"
		all=""
		for (( i = 1; i <= ${#gram[@]}; i++ )); do
			if [[ ${e} == ${gram[$i]%-*} ]]; then
				echo -e "  ${i}. ${gram[$i]}"
				all+="|${i}"
			fi
		done
		all+="|"
		printf "Применяем правило: "; read current
		while [[ -z $(echo ${all} | grep "|${current}|") ]]; do
			printf "\nТакое правило нельзя применить!\nПопробуйте ещё раз: "; read current
		done
		hist+="${current} "
		sub=${gram[$current]#*-}
		chain=${chain/$e/$sub}
		tmp=${func%%()*}
		pos=$(( ${#tmp} + 1))
		sub=$(echo ${sub} | sed -r 's/([A-Z])/\1()/g')
		func=${func:0:$pos}${sub}${func:$pos} 
		e=$(find)
	done
	chain=${chain//0/}
	func=${func//0/}
}

declare -A gram
input
chain=S
func="S()"
replace
echo -e "\nТерминальная цепочка:      ${chain}"
echo "Последовательность правил: ${hist}"
echo "ЛСФ ДВ:                    ${func}"