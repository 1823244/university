MASM
MODEL SMALL
.STACK 100h
.CODE
main PROC
	MOV AL, 20h
	MOV BL, 10h
	ADD AL, BL
	SUB BL, 3h
	AND AX, BX

	MOV AX, 4c00h
	INT 21h
main ENDP
END main