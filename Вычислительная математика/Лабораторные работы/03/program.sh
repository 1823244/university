## example: 
#
# func: (x/2 + 1) * s(x/2)
#    a: 1.2
#    b: 2.8

# predefined scale
scale=4

getstep() {
	echo $(echo "scale=${scale}; (${b} - ${a})/$1" | bc -l)
}

rectangle() {
	printf "eps = "
	read eps
	
	N=${n}
	new=0
	delta=${eps}
	while [[ $(echo "${delta} >= ${eps}" | bc) = 1 ]]; do
		h=$(getstep ${N})
		x=$(echo "scale=${scale}; ${a} + ${h} / 2" | bc -l)
		I=0
		for (( i = 0; i < N; i++ )); do
			I=$(echo "scale=${scale}; ${I} + ${func//x/$x}" | bc -l)
			x=$(echo "${x} + ${h}" | bc -l)
		done
		
		last=${new}
		new=$(echo "scale=${scale}; ${I} * ${h}" | bc -l)
		
		if [[ $N -eq $n ]]; then
			delta=${eps}
		else
			delta=$(echo "scale=${scale}; 1/3 * (${new} - ${last})" | bc -l)
			delta=${delta/-}

		fi
		N=$(echo "${N} * 2" | bc -l)
	done
	echo ${last}
}

trapezoidal() {
	printf "eps = "
	read eps
	
	N=${n}
	new=0
	delta=${eps}
	while [[ $(echo "${delta} >= ${eps}" | bc) = 1 ]]; do
		h=$(getstep ${N})
		I=$(echo "scale=${scale}; (${func//x/$a}) / 2" | bc -l)
		x=$(echo "${a} + ${h}" | bc -l)
		for (( i = 1; i < N; i++ )); do
			I=$(echo "scale=${scale}; ${I} + ${func//x/$x}" | bc -l)
			x=$(echo "${x} + ${h}" | bc -l)
		done
		I=$(echo "scale=${scale}; ${I} + (${func//x/$x}) / 2" | bc -l)

		last=${new}
		new=$(echo "${I} * ${h}" | bc -l)
		
		if [[ $N -eq $n ]]; then
			delta=${eps}
		else
			delta=$(echo "scale=${scale}; 1/3 * (${new} - ${last})" | bc -l)
			delta=${delta/-}

		fi
		N=$(echo "${N} * 2" | bc -l)
	done
	echo ${last}
}

simpson() {
	printf "eps = "
	read eps
	
	N=${n}
	new=0
	delta=${eps}
	while [[ $(echo "${delta} >= ${eps}" | bc) = 1 ]]; do
		h=$(getstep ${N})
		I=0
		halfX=$(echo "${a} + ${h}" | bc -l)
		x=${a}
		for (( i = 0; i < N; i++ )); do
			I=$(echo "scale=${scale}; ${I} + 2 * ${func//x/$x} + 4 * ${func//x/$halfX} + ${h}" | bc -l)
			x=$(echo "${x} + ${h}" | bc -l)
			halfX=$(echo "${halfX} + ${h}" | bc -l)
		done
		
		last=${new}
		new=$(echo "scale=${scale}; ${I} * ${h} / 6" | bc -l)
		
		if [[ $N -eq $n ]]; then
			delta=${eps}
		else
			delta=$(echo "scale=${scale}; 1/15 * (${new} - ${last})" | bc -l)
			delta=${delta/-}

		fi
		N=$(echo "${N} * 2" | bc -l)
	done
	echo ${last}
}

gauss() {
	printf "t = "
	#t=($(sed -n 'Np' './input' )) where N is number of string needed
	read -a t
	printf "A = "
	read -a A

	h=$(getstep ${n})
	middle=$(echo "scale=${scale}; (${b} + ${a}) / 2" | bc -l)
	I=0
	for (( i = 0; i < n; i++ )); do
		x=$(echo "${h} * ${t[i]} + ${middle}" | bc -l)
		I=$(echo "scale=${scale}; ${I} + ${A[i]} * ${func//x/$x}" | bc -l)
	done
	echo $(echo "${I} * ${h}" | bc -l)
}

choose() {
	PS3="Pick a method: "
	options=("Rectangle" "Trapezoidal" "Simpson" "Gauss")
	select opt in "${options[@]}" "Quit"; do
		case "$REPLY" in
			1)	rectangle; break;;
			2)	trapezoidal; break;;
			3)	simpson; break;;
			4)	gauss; break;;
			$(( ${#options[@]} + 1)) )	echo "Goodbye!"; break;;
			* )	echo "Invalid option. Try another one."; continue;;
		esac
	done
}

printf "F = "
read func
printf "a = "
read a
printf "b = "
read b
printf "n = "
read n
choose