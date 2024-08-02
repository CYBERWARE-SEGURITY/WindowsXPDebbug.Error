[BITS 16]
[ORG 0x7C00]

WSCREEN equ 320
HSCREEN equ 200

main:
    call setup
    call drawStars
    jmp $        ; Loop infinito

; Configuração inicial
setup:
    mov ah, 0x00    ; Função BIOS para alterar modo gráfico
    mov al, 0x13    ; Modo 13h (320x200 em 256 cores)
    int 0x10        ; Chama a BIOS

    push 0xA000     ; Define o segmento de vídeo para A000h
    pop es

    xor al, al      ; Limpa AL (cor inicial)
    xor bx, bx      ; Página de vídeo

    ret

; Sub-rotina para desenhar estrelas piscando
drawStars:
    mov cx, WSCREEN / 2    ; Define o centro horizontal da tela
    mov dx, HSCREEN / 2    ; Define o centro vertical da tela

drawLoop:
    mov ax, cx      ; Coloca coordenada X em AX
    add ax, dx      ; Adiciona coordenada Y
    xor ah, ah      ; Limpa AH

    xor bl, bl      ; Cor 0 (preto)
    int 0x10        ; Desenha pixel

    inc cx          ; Avança horizontalmente
    cmp cx, WSCREEN ; Verifica se chegou ao final da linha
    jl drawLoop     ; Repete até o fim da linha

    inc dx          ; Avança verticalmente
    cmp dx, HSCREEN ; Verifica se chegou ao final da tela
    jl drawStars    ; Repete até o fim da tela

    ret

; Assinatura do MBR
times 510 - ($ - $$) db 0
dw 0xAA55