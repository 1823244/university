#include "StackArray.h"

// Инициализация стека
void InitStack(Stack *S){
	S->Top = StackSize;
};

// Вставка в стек
void PutStack(Stack *S, BaseType E){
	if (S->Top == 0) {
		StackError = StackFull;
		return;
	}
	
	S->Buf[--S->Top] = E;
};

// Взятие из стека
void GetStack(Stack *S, BaseType *E){
	if (S->Top  == StackSize) {
		StackError = StackEmpty;
		return;
	}

	*E = S->Buf[S->Top++];
};

// Проверка стека на заполненность
bool FullStack(Stack *S) {
	if (S->Top == 0) {
		StackError = StackFull;
		return true;
	}
	return false;
}

// Проверка стека на пустоту
bool EmptyStack(Stack *S){
	if (S->Top == StackSize) {
		StackError = StackEmpty;
		return true;
	}
	return false;
};