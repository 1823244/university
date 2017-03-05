.386
.model flat, stdcall
option casemap :none 

include \masm32\include\windows.inc
include \masm32\include\user32.inc
include \masm32\include\kernel32.inc

includelib \masm32\lib\user32.lib
includelib \masm32\lib\kernel32.lib

.const

.data

 	Buffer			db 	512 dup(0)
 	Format			db 	"(8 * %d - 4 * %d + 92) / (%d - 4) = %d (%d)", 10, 0

 	a	dd	20 		; a
 	cc	dd 	100 		; c
 	d 	dd  -100 		; d

.code

	main PROC
	  	local stdout :dword

	  	invoke GetStdHandle, -11
	  	mov stdout, EAX

		MOV 	EAX, cc		; EAX = c
		IMUL 	EAX, 8		; EAX = c * 8

		MOV 	EBX, d 		; EBX = d
		IMUL 	EBX, 4		; EBX = 4 * d

		SUB 	EAX, EBX 	; EAX = (8c - 51d)
		ADD     EAX, 92		; EAX = (8c - 51d + 92)

		MOV 	EBX, a 		; EAX = a
		SUB 	EBX, 4 		; EAX = a - 4

		MOV	    EDX, 0      ; EDX = remainder = 0
		IDIV 	EBX 		; EAX / EBX = (8c - 51d + 92) / (a - 4)
							; EAX = result
							; EDX = remainder

		invoke wsprintf, ADDR Buffer, ADDR Format, cc, d, a, EAX, EDX
		invoke lstrlen, ADDR Buffer
		invoke WriteConsole, stdout, ADDR Buffer, EAX, NULL, NULL
	  	invoke Sleep, 5000d
		invoke ExitProcess, NULL

	main ENDP

	end main