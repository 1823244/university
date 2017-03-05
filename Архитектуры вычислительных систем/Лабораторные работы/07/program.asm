include \masm32\include\masm32rt.inc

.const

    EnterN          db  "number: ", 0
    InBin           db  "bin: ", 0

    NewLine         db  10, 0

    One             db  "1", 0
    Zero            db  "0", 0
    Space           db  " ", 0

    InputFormat     db  "%hd", 0
    ;OutputFormat    db  "result: %d", 0
    OutputFormat    db  "%d ", 0

.data

.data?

    n   dw  ?

.code

    inputing macro tip, format, variable
        invoke crt_printf, addr tip
        invoke crt_scanf, addr format, addr variable
    endm

    printing macro format, variable
        invoke crt_printf, addr format, variable
    endm

    printStr macro string
        invoke crt_printf, addr string
    endm

    main proc

        ;inputing EnterN, InputFormat, n

        ;PUSH n
        ;call printBin
        ;printStr NewLine

        ;PUSH n
        ;call first
        ;printStr NewLine

        ;PUSH n
        ;call second
        ;printStr NewLine

        call defense

        inkey
        invoke ExitProcess, NULL

    main endp

    defense proc

    	MOV EAX, 1

    	MOV ECX, 31
    	outer:
    		PUSH ECX
    		MOV EBX, EAX
    		DEC EBX

    		MOV EDX, 0
    		BTS EDX, EAX

    		MOV ECX, EAX
    		PUSH EDX

    		inner:	
    			POP EDX
    			PUSH EDX
    			BTS EDX, EBX
    			PUSH EAX
    			PUSH EBX
    			PUSH ECX

    			PUSH EDX
    			call printBin
    			printStr NewLine

    			POP ECX
    			POP ECX
    			POP EBX
    			POP EAX

    		endinner:
    		DEC EBX
    		LOOP inner

    	endouter:
    	POP EDX
    	INC EAX
       	POP ECX
    	LOOP outer

    	ret

    defense endp

    printBin proc

        printStr InBin      

        POP EBX             ; save return address
        MOV ECX, 32         ; ECX = 16, number of bits in word 
        MOV ESI, 0          ; printed digits = 0
        MOV EDI, 4          ; digits in one part = 4

        cycle:
            POP EDX          ; get n
            ROL EDX, 1       ; cyclic shift DX to left; left bit to CF 
            PUSH EDX         ; save n
            PUSH ECX        ; save ECX

            JB printOne     ; if CF == 1 then print `1`

            printStr Zero   ; print `0`
            JMP endcycle    ; to end of loop

        printOne:
            printStr One    ; print `1`

        endcycle:
            INC ESI         ; printed digits += 1
            CMP ESI, EDI    ; ESI == EDI ? ZF = 1 
            JE printSpace   ; if ESI == EDI then print ` `
            JMP endd

        printSpace:
            MOV ESI, 0      ; printed digits = 0
            printStr Space  ; print ` `
            
        endd:
        POP ECX             ; restore ECX
        LOOP cycle           

        PUSH EBX            ; restore return address
        ret

    printBin endp

    first proc

        POP EBX             ; save return address
        POP AX              ; n

        ; for example:  n = 1000, n_2 = 0000 0011 1110 1000
        BTS AX, 0           ; n_2 = 0000 0011 1110 100[1]    
        BTS AX, 2           ; n_2 = 0000 0011 1110 1[1]01
        BTS AX, 5           ; n_2 = 0000 0011 11[1]0 1101
        BTR AX, 7           ; n_2 = 0000 0011 [0]110 1101
        BTR AX, 11          ; n_2 = 0000 [0]011 0110 1101
        BTR AX, 13          ; n_2 = 00[0]0 0011 0110 1101
        BTC AX, 3           ; n_2 = 0000 0011 0110 [0]101
        BTC AX, 8           ; n_2 = 0000 001[0] 0110 0101
        BTC AX, 15          ; n_2 = [1]000 0010 0110 0101
        ; result: n_2 = 1000 0010 0110 0101, n = 33381

        printing OutputFormat, AX

        endd: 
        PUSH EBX            ; restore return address
        ret

    first endp

    second proc

        POP EBX             ; save return address
        POP AX              ; n

        ; for example: n = 1000, n_2 = 0000 0011 1110 1000
        BSF DX, AX          ; n_2 = 0000 0011 1110 1000 
                            ;                      ^ third bit equals 1
                            ;                        => DX = 3        
        BTR AX, DX          ; n_2 = 0000 0011 1110 [0]000
        ; result: n_2 = 0000 0011 1110 1000, n = 992

        printing OutputFormat, AX

        endd: 
        PUSH EBX            ; restore return address
        ret

    second endp    

    end main