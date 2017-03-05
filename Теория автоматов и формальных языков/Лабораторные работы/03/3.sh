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

	for (( i = 0; i < ${#accept[@]}; i++ )); do
		if [[ s -eq ${accept[$i]} ]]; then
			echo "1"
			return	
		fi
	done

	echo "0"
}

while read string; do
	action=$(interpretation)
	if [[ action -eq 0 ]]; then
		echo $string >> input2.tmp
	fi
done < "input2"

rm input2
mv input2.tmp input2