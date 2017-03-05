#include "Deque.h"

// Инициализация дека
void InitDeque(Deque *D) {
	InitDList(D);
	DequeError = DListError;
}

// Вставка в начало дека
void PutDequeFront(Deque *D, BaseType E) {
	BeginPtr(D);
	PutDListR(D, E);
	DequeError = DListError;
}

// Вставка в конец дека
void PutDequeEnd(Deque *D, BaseType E) {
	EndPtr(D);
	PutDListL(D, E);
	DequeError = DListError;
}

// Взятие из начала дека 
void GetDequeFront(Deque *D, BaseType *E) {
	MoveTo(D, 1);
	GetDListR(D, E);
	DequeError = DListError;
}

// Взятие из конца дека
void GetDequeBack(Deque *D, BaseType *E) {
	EndPtr(D);
	GetDListL(D, E);
	DequeError = DListError;
}

// Проверка на пустоту
bool EmptyDeque(Deque *D) {
	return EmptyDList(D);
}

// Очистка дека
void DoneDeque(Deque *D) {
	ClearDList(D);
	DequeError = DListError;
}