#ifndef DEQUE_DLL_H
#define DEQUE_DLL_H

#include "DLL.h"

const short DequeOk = DListOk;
const short DequeNotMem = DListNotMem;
const short DequeEmpty = DListEmpty;
const short DequeEnd = DListEnd;
static short DequeError;

typedef DList Deque;

// Инициализация дека
void InitDeque(Deque *D);

// Вставка в начало дека
void PutDequeFront(Deque *D, BaseType E);

// Вставка в конец дека
void PutDequeEnd(Deque *D, BaseType E);

// Взятие из начала дека
void GetDequeFront(Deque *D, BaseType *E);

// Взятие из конца дека
void GetDequeBack(Deque *D, BaseType *E);

// Проверка на пустоту
bool EmptyDeque(Deque *D);

// Очистка дека
void DoneDeque(Deque *D);

#endif