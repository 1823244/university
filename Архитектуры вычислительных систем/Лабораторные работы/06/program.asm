include \masm32\include\masm32rt.inc

.const

    two     dq  2.
    five    dq  5.
    twfive  dq  25.

    enterA  db  "a: ", 0
    enterB  db  "b: ", 0
    enterH  db  "h: ", 0
    enterN  db  "n: ", 0

    PrintArray  db  "Printing..", 10, 0
    NewLine     db  10, 0

    InputFormat     db  "%lf", 0
    InputNFormat    db  "%d", 0

    OutputFormat    db  "%.3f ", 0

.data

    l   dd  0
    arr dq  50 dup(0) 

.data?

    a   dq  ?
    b   dq  ?
    h   dq  ?
    n   dd  ?
    tmp dq  ?

.code

    inputing macro tip, format, variable
        invoke crt_printf, addr tip
        invoke crt_scanf, addr format, addr variable
    endm

    main proc
        FINIT           ; init of coprocessor

        inputing enterA, InputFormat, a
        inputing enterB, InputFormat, b
        inputing enterH, InputFormat, h
        inputing enterN, InputNFormat, n

        MOV ECX, n      ; ECX = n = number of loop repetition
        MOV EDI, 0      ; EDI = 0
        
        FLD a           ; ST(0) = a

        cycle:
            
            FCOM b      ; compare a and b, result in SWR
                        ; C2 = 1 => incomparable
                        ; C0 = 1 => a < b
                        ; C3 = 1 => a = b
                        ; else   => a > b 
            FSTSW AX    ; SWR to AX
            SAHF        ; ZF = C3, PF = C2, CF = C0

            JP endc
            JC less
            JZ equal
            ; else a > b:

            FLD b       ; ST(0) = b, ST(1) = a
            FDIVR       ; ST(0) = b / a 
            FADD two    ; ST(0) = 2 + b / a

            JMP endc

        less:
            FSUB five   ; ST(0) = a - 5
            FDIV b      ; ST(0) = (a - 5) / b

            JMP endc

        equal:
            FSTP tmp    ; ST is empty
            FLD twfive  ; ST(0) = 25

            JMP endc

        endc:
            FSTP arr[EDI]   ; arr[] = ..
            ADD EDI, 8      ; increase index 
            ADD l, 1        ; increase counter of elements

            FLD a           ; ST(0) = a
            FADD h          ; ST(0) = a + h
            FST a           ; a = ST(0)
            LOOP cycle


        invoke crt_printf, addr PrintArray
        PUSH offset arr
        PUSH l
        CALL show   ; print array
        invoke crt_printf, addr NewLine

        PUSH offset arr
        PUSH l
        CALL sort   ; sort array

        invoke crt_printf, addr PrintArray
        PUSH offset arr
        PUSH l
        CALL show   ; print array
        invoke crt_printf, addr NewLine

        inkey
        invoke ExitProcess, NULL

    main endp

    show proc 
        POP EBX             ; save return address
        POP ECX             ; get length
        POP ESI             ; get array offset

        MOV EAX, ECX        ; copy length
        printer:
            PUSH ECX        ; save current length
            PUSH EAX        ; save init length
            SUB EAX, ECX    ; calculate element offset (in bytes)
            invoke crt_printf, addr OutputFormat, QWORD PTR [ESI + EAX * 8]
            POP EAX         ; restore copy of length
            POP ECX         ; restore length

        LOOP printer

        PUSH EBX            ; restore return address

        ret

    show endp

    sort proc

        POP EBX             ; save return address
        POP ECX             ; get length
        POP ESI             ; get array offset
        PUSH EBX            ; return return address

        MOV EDX, ECX        ; copy length

        outer:
            CMP ECX, 3      ; ECX < 3?
            JL endd         ; if above == true go to the end
            PUSH ECX        ; save length for outer

            MOV ECX, EDX    ; init current length for inner

            inner:
                CMP ECX, 3      ; ECX < 3?
                JL outerend         ; if above == true go to the end
                PUSH ECX        ; save current length
                PUSH EDX        ; save initial length
                SUB EDX, ECX    ; calculate element offset (in bytes)

                MOV EBX, EDX    ; EBX = EDX
                IMUL EBX, 8     ; EBX = EDX * 8
                ADD EBX, ESI    ; EBX = ESI + EDX * 8 — address of current element

                FLD QWORD PTR [EBX]       ; current element to ST
                FLD QWORD PTR [EBX + 16]  ; next element to ST
                FCOMPP      ; compare ST(0) and ST(1), result in SWR
                            ; C2 = 1 => incomparable
                            ; C0 = 1 => current > next
                            ; C3 = 1 => current = next
                            ; else   => current < next
                FSTSW AX    ; SWR to AX
                SAHF        ; ZF = C3, PF = C2, CF = C0
                
                ; if current <= next or these are incomparable
                JC innerend   
                JZ innerend
                JP innerend

                ; else if current > next
                    ; swapping
                    FLD QWORD PTR [EBX]
                    FLD QWORD PTR [EBX + 16]
                    FSTP QWORD PTR [EBX]
                    FSTP QWORD PTR [EBX + 16]

                    JMP innerend

                innerend:
                POP EDX     ; restore initial length
                POP ECX     ; restore current length

                DEC ECX     ; escape one more element
                LOOP inner

            outerend:
            POP ECX     ; restore length for outer
            DEC ECX     ; escape one more element
            LOOP outer

        endd: 

        ret

    sort endp

    end main