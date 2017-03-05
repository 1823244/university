#euler(x, y)
e() {
	echo $(echo "
		scale=$e
		define f(x, y) { return ($func) }
		define e(x, y, h) { return (y + h * f(x, y)) }
		e($1, $2, $h)" | bc -l)
}

#cauchy-euler(x, y)
ce() {
	echo $(echo "
		scale=$e
		define f(x, y) { return ($func) }
		define ce(x, y, h) { return (y + h/2 * (f(x, y) + f(x + h, y + h * f(x, y)))) }
		ce($1, $2, $h)" | bc -l)
}

#mod-euler(x, y)
me() {
	echo $(echo "
		scale=$e
		define f(x, y) { return ($func) }
		define me(x, y, h) { return (y + h * f(x + h/2, y + h/2 * f(x, y))) }
		me($1, $2, $h)" | bc -l)
}

#runge-kutta(x, y)
rk() {
	echo $(echo "
		scale=$e
		define f(x, y) { return ($func) }
		define m(x, y) { return f(x, y) }
		define m2(x, y, h) { return f(x + h/2, y + h/2 * m(x, y)) }
		define m3(x, y, h) { return f(x + h/2, y + h/2 * m2(x, y, h)) }
		define m4(x, y, h) { return f(x + h, y + h * m3(x, y, h)) }
		define rk(x, y, h) { return (y + h/6 * (m(x, y) + 2 * m2(x, y, h) + 2 * m3(x, y, h) + m4(x, y, h))) }
		rk($1, $2, $h)" | bc -l)
}

printf "f(x, y) = "; read func
printf "y(a) = "; read y0
printf "a = "; read a
printf "b = "; read b
printf "n = "; read n
printf "e = "; read e

h=$(echo "scale=3; ($b - $a) / $n" | bc -l)
printf "%2s%6s%8s%8s%8s%8s\n" "i" "x" "y[e]" "y[ce]" "y[me]" "y[rk]"

x=$a; ye=$y0; yce=$y0; yme=$y0; yrk=$y0
printf "%2s%7s%7s%8s%8s%8s\n" "0" "$x" "$ye" "$yce" "$yme" "$yrk"
for (( i = 1; i <= $n; i++ )); do
	ye=$(e $x $ye)
	yce=$(ce $x $yce)
	yme=$(me $x $yme)
	yrk=$(rk $x $yrk)
	x=$(echo "$x + $h" | bc -l)
	printf "%2s%7s%7s%8s%8s%8s\n" "$i" "$x" "$ye" "$yce" "$yme" "$yrk"
done