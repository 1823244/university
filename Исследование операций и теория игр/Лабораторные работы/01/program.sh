enter() {
	read equation
	local index=0
	local length=0
	while [[ ${equation:0:1} != 'z' ]]; do
		matrix[$index, 0]=${equation##*=}
		array=(${equation//[+=]/ })
		local ratio=1
		for (( i = 0; i < (${#array[@]} - 1); i++ )); do
			if [[ ${array[$i]} == "-" ]]; then
				ratio=-1
				continue
			fi
			element=(${array[i]//x/ })	
			if [[ ${#element[@]} -eq 1 ]]; then
				element[1]=${element[0]}
				element[0]=1
			fi
			[[ ${element[0]} == "-" ]] && element[0]=-1
			echo "${i}: ${element[@]}"
			[[ ${element[1]} -gt ${length} ]] && length=${element[1]}
			matrix[$index, ${element[1]}]=$(echo "${element[0]} * ${ratio}" | bc)
			ratio=1
		done
		let index++
		read equation
	done
	func=${equation}

	let length++
	for (( i = 0; i < ${index}; i++ )); do
		for (( j = 0; j < ${length}; j++ )); do
			if [[ -z ${matrix[$i, $j]} ]]; then
				matrix[${i}, ${j}]=0
			fi
		done
	done

	height=${index}
	width=${length}
}

check() {
	echo ${#m[@]}
	for (( i = 0; i < ${height}; i++ )); do
		local notzeroes=0
		for (( j = 0; j < ${width}; j++ )); do
			[[ "${m[$i, $j]}" != "0" ]] && let notzeroes++
		done
		if [[ ${notzeroes} -eq 0 ]]; then
			let height--
			for (( j = 0; j < ${width}; j++ )); do
				m[${i}, ${j}]=${m[$height, $j]}
			done
		fi
		[[ ${notzeroes} -eq 1 ]] && [[ ${m[$i, 0]} -ne 0 ]] && { echo "System is inconsistent"; exit; }
	done
}

zeroing() {
	row=$1
	let row--
	col=$2
	eval $(declare -Ap matrix | sed 's/ matrix=/ m=/')
	if [[ ${m[$row, $col]} -ne 0 ]]; then
		for (( i = 0; i < ${width}; i++ )); do
			# echo "scale=2; ${m[$row, $i]} / ${m[$row, $col]}"
			m[${row}, ${i}]=$(echo "scale=2; ${m[$row, $i]} / ${m[$row, $col]}" | bc)
		done

		for (( i = 0; i < ${height}; i++ )); do
			[[ ${i} -eq ${row} ]] && continue
			# echo "- ${m[$i, $col]}"
			local ratio=$(echo "- ${m[$i, $col]}" | bc)
			for (( j = 0; j < ${width}; j++ )); do
				# echo "${m[$i, $j]} + ${ratio} * ${m[$row, $j]}"
				m[${i}, ${j}]=$(echo "${m[$i, $j]} + ${ratio} * ${m[$row, $j]}" | bc)
			done
		done	
	fi

	check

	# for (( i = 0; i < ${height}; i++ )); do
	# 	for (( j = 0; j < ${width}; j++ )); do
	# 		printf "%-10s" ${m[$i, $j]}
	# 	done
	# 	echo ""
	# done
}

#generator() {
	# получает два значения: i и j
	# идет от i + 1 и от j + 1 до чего-то достаточного
	# по умолчанию i и j должны быть 0
#}


declare -A matrix
enter
#zeroing 1 1