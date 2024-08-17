[BITS 16]
[ORG 0x7C00]

WSCREEN equ 320
HSCREEN equ 200

main:
    call setup
    call drawStars
    jmp $        ; Infinite loop

; Initial setup
setup:
    mov ah, 0x00    ; BIOS function to change graphics mode
    mov al, 0x13    ; Mode 13h (320x200 in 256 colors)
    int 0x10        ; Call BIOS

    push 0xA000     ; Set video segment to A000h
    pop es

    xor al, al      ; Clear AL (initial color)
    xor bx, bx      ; Video page

    ret

drawStars:
    mov cx, WSCREEN / 2    ; Set horizontal center of screen
    mov dx, HSCREEN / 2    ; Set vertical center of screen

drawLoop:
    mov ax, cx      ; Place X coordinate in AX
    add ax, dx      ; Add Y coordinate
    xor ah, ah      ; Clear AH

    xor bl, bl      ; Color 0 (black)
    int 0x10        ; Draw pixel

    inc cx          ; Move horizontally
    cmp cx, WSCREEN ; Check if end of line reached
    jl drawLoop     ; Repeat until end of line

    inc dx          ; Move vertically
    cmp dx, HSCREEN ; Check if end of screen reached
    jl drawStars    ; Repeat until end of screen

    ret

; MBR signature
times 510 - ($ - $$) db 0
dw 0xAA55
