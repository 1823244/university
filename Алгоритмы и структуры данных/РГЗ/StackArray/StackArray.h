#ifndef STACK_ARRAY_H
#define STACK_ARRAY_H

#include <stdio.h>
#include <stdlib.h>

const short StackSize = 1000;
const short StackOk = 0;
const short StackEmpty = 1;
const short StackFull = 2;
static short StackError;

typedef int BaseType;
typedef struct {
	BaseType Buf[StackSize];
	unsigned Top;
} Stack;

// Инициализация стека
void InitStack(Stack *S);

// Вставка в стек
void PutStack(Stack *S, BaseType E);

// Взятие из стека
void GetStack(Stack *S, BaseType *E);

// Проверка стека на заполненность
bool FullStack(Stack *S);

// Проверка стека на пустоту
bool EmptyStack(Stack *S);

#endif