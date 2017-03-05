entropy() { 
	total=0
	array=("$@")
	for (( i=0; i<${#array[@]}; i++ )); do
		total=$(echo "scale=5; ${total} + ${array[$i]} * l(${array[$i]})/l(2)" | bc -l)
	done
	total=$(echo "- ${total}" | bc)
	echo $total
}

read -a arr
result=$(entropy ${arr[@]})
echo $result