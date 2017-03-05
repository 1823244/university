include \masm32\include\masm32rt.inc

.const
	four	dq	4.
	eight 	dq 	8.
	twthree dq 	23.

	enterA  db "a: ", 0
	enterC  db "c: ", 0
	enterD  db "d: ", 0

	OutputFormat	db 	"(8 * %.3f - 4 * %.3f + 4 * sqrt(23 * %.3f)) / (%.3f - 4) = %.3f", 10, 0
 	InputFormat		db	"%lf", 0

.data

.data? 
	a	dq	? 		; a
 	cc	dq 	? 		; c
 	d 	dq  ? 		; d
 	res dq 	?		; result

.code

	main PROC
	  	FINIT			; init of coprocessor

	  	invoke crt_printf, addr enterA
	  	invoke crt_scanf, addr InputFormat, addr a
	  	
	  	invoke crt_printf, addr enterC
	  	invoke crt_scanf, addr InputFormat, addr cc

	  	invoke crt_printf, addr enterD
	  	invoke crt_scanf, addr InputFormat, addr d
	  	
		FLD cc 			; ST(0) = c
		FMUL eight		; ST(0) = 8 * c

		FLD d 			; ST(0) = d, ST(1) = 8 * c
		FMUL four		; ST(0) = 4 * d, ST(1) = 8 * c

		FSUB			; ST(0) = 8 * c - 4 * d

		FLD a 			; ST(0) = a, ST(1) = 8 * c - 4 * d
		FMUL twthree	; ST(0) = 23 * a, ST(1) = 8 * c - 4 * d
		FSQRT 			; ST(0) = sqrt(23 * a), ST(1) = 8 * c - 4 * d
		FMUL four		; ST(0) = 4 * sqrt(23 * a), ST(1) = 8 * c - 4 * d

		FADD			; ST(0) = 8 * c - 4 * d + 4 * sqrt(23 * a)

		FLD a 			; ST(0) = a, ST(1) = 8 * c - 4 * d + 4 * sqrt(23 * a)
		FSUB four		; ST(0) = a - 4, ST(1) = 8 * c - 4 * d + 4 * sqrt(23 * a)		

		FDIV			; ST(0) = (8 * c - 4 * d + 4 * sqrt(23 * a)) / (a - 4)

		FSTP res 		; res = (8 * c - 4 * d + 4 * sqrt(23 * a)) / (a - 4)

		invoke crt_printf, offset OutputFormat, cc, d, a, a, res

		inkey
		invoke ExitProcess, NULL

	main ENDP

	end main