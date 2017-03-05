include \masm32\include\masm32rt.inc

.code

    ; main is dll error handler
    main proc hInstance: DWORD, reason: DWORD, reserved1: DWORD 
        MOV EAX, 1
        ret
    main endp 

    ; find positive average
    ; `arr` is double array
    ; `l` is length of array
    findAvg proc arr: DWORD, l: DWORD

        .data
            neg1    dq -1.

        .data?
            n   dd ?
            tmp dq ?
            avg dq ?

        .code
            FINIT

            MOV ECX, l          ; save length to ECX
            MOV ESI, arr        ; save pointer to array to ESI

            MOV EAX, ECX        ; copy length
            MOV EDI, 0          ; init counter of 0+ elements
            FLDZ                ; init sum (ST(0) = 0)

            PUSH EAX

            cycle:
                POP EAX
                PUSH EAX
                
                SUB EAX, ECX    ; calculate element offset (in bytes)
                FLD QWORD PTR [ESI + EAX * 8]   ; element to fstack
                FTST           ; ST(0) <= 0?
                FSTSW AX       ; SWR to AX
                SAHF           ; AX to flags

                JA plus        ; if greater                
                
                ; else: 
                FSTP tmp       ; pop ST(0)
                JMP endc

                plus:
                FADD           ; sum += current (ST(0) = ST(0) + ST(1))
                ADD EDI, 1     ; counter++

            endc: 

            LOOP cycle

            CMP EDI, 0          ; EDI <= 0 ?
            JLE error           ; if above == true go to the error

            MOV n, EDI          ; n = EDI
            FILD n              ; ST(0) = n
            FDIV                ; ST(0) = ST(1) / ST(0)

            JMP enda

            error:

            FLD neg1            ; return -1 if array does not contain positive elements

            enda:

            ; if func signature returns `double` func returns ST(0).
            ; so now in ST(0) we have avg (or -1 if we have error).
            ret

    findAvg endp

end main