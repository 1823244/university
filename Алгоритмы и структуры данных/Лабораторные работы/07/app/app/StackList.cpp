#include "StackList.h"

// ������������� �����
void InitStack(Stack *S) {
	InitList(S);
	StackError = ListError;
}

// ������� � ����
void PutStack(Stack *S, BaseType E) {
	PutList(S, E);
	StackError = ListError;
}

// ������ �� �����
void GetStack(Stack *S, BaseType *E) {
	GetList(S, E);
	StackError = ListError;
}

// ������� �����
void DoneStack(Stack *S) {
	ClearList(S);
	StackError = ListError;
}

// �������� �� �������
bool EmptyStack(Stack *S) {
	return EmptyList(S);
}