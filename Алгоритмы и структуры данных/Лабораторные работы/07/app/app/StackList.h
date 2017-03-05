#ifndef STACK_LIST_H
#define STACK_LIST_H

#include "SLL.h"

const short StackOk = ListOk;
const short StackEmpty = ListEmpty;
const short StackNotMem = ListNotMem;
static short StackError;

typedef List Stack;

// ������������� �����
void InitStack(Stack *S);

// ������� � ����
void PutStack(Stack *S, BaseType E);

// ������ �� �����
void GetStack(Stack *S, BaseType *E);

// ������� �����
void DoneStack(Stack *S);

// �������� �� �������
bool EmptyStack(Stack *S);

#endif