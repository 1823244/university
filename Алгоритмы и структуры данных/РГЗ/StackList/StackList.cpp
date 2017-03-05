#include "StackList.h"

// Инициализация стека
void InitStack(Stack *S) {
	InitList(S);
	StackError = ListError;
}

// Вставка в стек
void PutStack(Stack *S, BaseType E) {
	PutList(S, E);
	StackError = ListError;
}

// Взятие из стека
void GetStack(Stack *S, BaseType *E) {
	GetList(S, E);
	StackError = ListError;
}

// Очистка стека
void DoneStack(Stack *S) {
	ClearList(S);
	StackError = ListError;
}

// Проверка на пустоту
bool EmptyStack(Stack *S) {
	return EmptyList(S);
}