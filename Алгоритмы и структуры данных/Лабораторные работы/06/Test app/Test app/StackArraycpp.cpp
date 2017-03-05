#include "StackArray.h"

// ������������� �����
void InitStack(Stack *S) {
	S->Top = StackSize;
	StackError = StackOk;
};

// ������� � ����
void PutStack(Stack *S, BaseType E) {
	if (S->Top == 0) {
		StackError = StackFull;
		return;
	}

	S->Buf[--S->Top] = E;
};

// ������ �� �����
void GetStack(Stack *S, BaseType *E) {
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return;
	}

	*E = S->Buf[S->Top++];
};

// ������ �� �����
void ReadStack(Stack *S, BaseType *E) {
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return;
	}

	*E = S->Buf[S->Top];
}

// �������� ����� �� �������������
bool FullStack(Stack *S) {
	if (S->Top == 0) {
		StackError = StackFull;
		return true;
	}
	return false;
}

// ����� ����������� �����
void PrintStack(Stack *S) {
	BaseType e;
	Stack tmpStack;

	InitStack(&tmpStack);

	while (!EmptyStack(S)) {
		GetStack(S, &e);
		PutStack(&tmpStack, e);
	}

	while (!EmptyStack(&tmpStack)) {
		GetStack(&tmpStack, &e);
		printf("\t--(%s, %d, %d)\n", e.Name, e.Time, e.P);
		PutStack(S, e);
	}
}

// �������� ����� �� �������
bool EmptyStack(Stack *S) {
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return true;
	}
	return false;
};