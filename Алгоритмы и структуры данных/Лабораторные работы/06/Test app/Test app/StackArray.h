#ifndef STACK_ARRAY_H
#define STACK_ARRAY_H

#include <stdio.h>
#include <stdlib.h>
#include "TInquiry.h"

const short StackSize = 1000;
const short StackOk = 0;
const short StackEmpty = 1;
const short StackFull = 2;
static short StackError;

typedef TInquiry BaseType;

typedef struct {
	BaseType Buf[StackSize];
	unsigned Top;
} Stack;

// ������������� �����
void InitStack(Stack *S);

// ������� � ����
void PutStack(Stack *S, BaseType E);

// ������ �� �����
void GetStack(Stack *S, BaseType *E);

// ������ �� �����
void ReadStack(Stack *S, BaseType *E);

// ����� ����������� �����
void PrintStack(Stack *S);

// �������� ����� �� �������������
bool FullStack(Stack *S);

// �������� ����� �� �������
bool EmptyStack(Stack *S);

#endif