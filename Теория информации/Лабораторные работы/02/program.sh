#define
scale=4

getArrayLength() {
	array=("$@")
	echo "$(echo "sqrt(${#array[@]})" | bc)"
}

readArray() {
	read -a arr
	echo ${arr[@]}
}

getEntropy() {
	HA=0; HB=0; HA_B=0; HB_A=0; HAB=0; IAB=0;

	for (( i = 0; i < ABLength; i++ )); do
		HA=$(echo "scale=${scale}; ${HA} - ${a[$i]} * l(${a[$i]})/l(2)" | bc -l)
		HB=$(echo "scale=${scale}; ${HB} - ${b[$i]} * l(${b[$i]})/l(2)" | bc -l)
		HA_B_part=0
		HB_A_part=0
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			HA_B_part=$(echo "scale=${scale}; ${HA_B_part} - ${A_B[$current]} * l(${A_B[$current]})/l(2)" | bc -l)
			HB_A_part=$(echo "scale=${scale}; ${HB_A_part} - ${B_A[$current]} * l(${B_A[$current]})/l(2)" | bc -l)
			HAB=$(echo "scale=${scale}; ${HAB} - ${AB[$current]} * l(${AB[$current]})/l(2)" | bc -l)
		done
		HA_B=$(echo "${HA_B} + ${b[$i]} * ${HA_B_part}" | bc -l)
		HB_A=$(echo "${HB_A} + ${a[$i]} * ${HB_A_part}" | bc -l)
	done
	IAB=$(echo "${HA} + ${HB} - ${HAB}" | bc -l)
	printf "H(A) = ${HA}\nH(B) = ${HB}\nH(A/B) = ${HA_B}\nH(B/A) = ${HB_A}\nH(A;B) = ${HAB}\nI(A;B) = ${IAB}\n"
}

A_Bb() {
	echo "Enter the P(A/B) matrix row by row:"
	A_B=($(readArray))
	ABLength=$(getArrayLength ${A_B[@]})
	echo "Enter the P(B) array:"
	b=($(readArray))

	echo "P(A;B):"
	for (( i = 0; i < ABLength; i++ )); do
		a[${i}]=0
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			AB[${current}]=$(echo "scale=${scale}; ${A_B[$current]} * ${b[$j]}" | bc)
			a[${i}]=$(echo "scale=${scale}; ${a[$i]} + ${AB[$current]}" | bc)
			printf "%-7s" ${AB[$current]}
		done
		printf "\n"		
	done

	printf "\n"

	echo "P(A):"
	for (( i = 0; i < ABLength; i++ )); do
		printf "%-7s" ${a[$i]}
	done
	
	printf "\n\n"

	echo "P(B/A):"
	for (( i = 0; i < ABLength; i++ )); do
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			#echo "current = ${i} * ${ABLength} + ${j} = ${current}"
			B_A[${current}]=$(echo "scale=${scale}; ${AB[$current]} / ${a[$i]}" | bc)
			#echo "B_A[${current}] = ${AB[$current]} / ${a[$i]} = ${B_A[current]}"
			printf "%-7s" ${B_A[$current]}
		done
		echo ""		
	done
	echo ""	
	getEntropy
}

B_Aa() {
	echo "Enter the P(B/A) matrix row by row:"
	B_A=($(readArray))
	ABLength=$(getArrayLength ${B_A[@]})
	echo "Enter the P(A) array:"	
	a=($(readArray))

	for (( i = 0; i < ABLength; i++ )); do
		b[${i}]=0
	done

	echo "P(A;B):"
	for (( i = 0; i < ABLength; i++ )); do
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			AB[${current}]=$(echo "scale=${scale}; ${B_A[$current]} * ${a[$i]}" | bc)
			b[${j}]=$(echo "scale=${scale}; ${b[$j]} + ${AB[$current]}" | bc)
			printf "%-7s" ${AB[$current]}
		done
		printf "\n"		
	done

	printf "\n"

	echo "P(B):"
	for (( i = 0; i < ABLength; i++ )); do
		printf "%-7s" ${b[$i]}
	done
	
	printf "\n\n"

	echo "P(A/B):"
	for (( i = 0; i < ABLength; i++ )); do
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			#echo "current = ${i} * ${ABLength} + ${j} = ${current}"
			A_B[${current}]=$(echo "scale=${scale}; ${AB[$current]} / ${b[$j]}" | bc)
			#echo "A_B[${current}] = ${AB[$current]} / ${b[$j]} = ${A_B[current]}"
			printf "%-7s" ${A_B[$current]}
		done
		echo ""		
	done
	echo ""	
	getEntropy
}

AB() {
	echo "Enter the P(A;B) matrix row by row:"	
	AB=($(readArray))
	ABLength=$(getArrayLength ${AB[@]})

	for (( i = 0; i < ABLength; i++ )); do
		a[${i}]=0
		b[${i}]=0
	done

	for (( i = 0; i < ABLength; i++ )); do
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			a[${i}]=$(echo "${a[$i]} + ${AB[$current]}" | bc)
			b[${j}]=$(echo "${b[$j]} + ${AB[$current]}" | bc)
		done
	done
	
	echo "P(A):"
	for (( i = 0; i < ABLength; i++ )); do
		printf "%-7s" ${a[$i]}
		bPart=$(printf "%-7s" ${b[$i]})
		bResult=$(printf "%s%-7s" "${bResult}" "${b[$i]}")
	done

	printf "\n\nP(B):\n%s\n\n" "${bResult}"

	echo "P(A/B):"
	for (( i = 0; i < ABLength; i++ )); do
		for (( j = 0; j < ABLength; j++ )); do
			current=$(echo "${i} * ${ABLength} + ${j}" | bc)
			A_B[${current}]=$(echo "scale=${scale}; ${AB[$current]} / ${b[$j]}" | bc)
			printf "%-7s" ${A_B[$current]}
			B_A[${current}]=$(echo "scale=${scale}; ${AB[$current]} / ${a[$i]}" | bc)
			baResult=$(printf "%s%-7s" "${baResult}" "${B_A[$current]}")
		done
		echo ""
		baResult+=$'\n'
	done

	printf "\n\nP(B/A):\n%s\n" "${baResult}"
	getEntropy
}

clear
PS3="Pick an option: "
options=("P(A/B) and P(B)" "P(B/A) and P(A)" "P(A;B)")
select opt in "${options[@]}" "Quit"; do
	case "$REPLY" in
		1 ) A_Bb; break;;
		2 )	B_Aa; break;;
		3 )	AB; break;;
		$(( ${#options[@]} + 1)) )	echo "Goodbye!"; break;;
		* )	echo "Invalid option. Try another one."; continue;;
	esac
done