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
	spaces=1
	while [[ (-n ${e}) && (${spaces} -ne 0) ]]; do
		all=""
		for (( i = 1; i <= ${#gram[@]}; i++ )); do
			if [[ ${e} == ${gram[$i]%-*} ]]; then
				all+="|${i}"
			fi
		done
		all+="|"
		current=${rules%% *}
		l=$(( ${#current} + 1))
		rules=${rules:l}
		spaces=$(echo "${rules}" | sed 's: : \n:g' | grep -c ' ')
		while [[ -z $(echo ${all} | grep "|${current}|") ]]; do
			echo "Результат: нет"
			exit
		done
		sub=${gram[$current]#*-}
		chain=${chain/$e/$sub}
		e=$(find)
	done
}

declare -A gram
input
chain=S
printf "Введите правила: "; read rules
replace
echo "Результат: да"