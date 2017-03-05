#ifndef STACK_LIST_H
#define STACK_LIST_H

#include "SLL.h"

const short StackOk = ListOk;
const short StackEmpty = ListEmpty;
const short StackNotMem = ListNotMem;
static short StackError;

typedef List Stack;

// Инициализация стека
void InitStack(Stack *S);

// Вставка в стек
void PutStack(Stack *S, BaseType E);

// Взятие из стека
void GetStack(Stack *S, BaseType *E);

// Очистка стека
void DoneStack(Stack *S);

// Проверка на пустоту
bool EmptyStack(Stack *S);

#endif