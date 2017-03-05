#include "StackArray.h"

// Инициализация стека
void InitStack(Stack *S) {
	S->Top = StackSize;
	StackError = StackOk;
};

// Вставка в стек
void PutStack(Stack *S, BaseType E) {
	if (S->Top == 0) {
		StackError = StackFull;
		return;
	}

	S->Buf[--S->Top] = E;
};

// Взятие из стека
void GetStack(Stack *S, BaseType *E) {
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return;
	}

	*E = S->Buf[S->Top++];
};

// Чтение из стека
void ReadStack(Stack *S, BaseType *E) {
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return;
	}

	*E = S->Buf[S->Top];
}

// Проверка стека на заполненность
bool FullStack(Stack *S) {
	if (S->Top == 0) {
		StackError = StackFull;
		return true;
	}
	return false;
}

// Вывод содержимого стека
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

// Проверка стека на пустоту
bool EmptyStack(Stack *S) {
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return true;
	}
	return false;
};