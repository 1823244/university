#ifndef DLL_H
#define DLL_H

#include <stdio.h>
#include <stdlib.h>

const short DListOk = 0;
const short DListNotMem = 1;
const short DListEmpty = 2;
const short DListEnd = 3;
static short DListError;

typedef int BaseType;
typedef struct element * ptrel;
typedef struct element {
	BaseType data;
	ptrel next;
	ptrel prev;
} element;
typedef struct {
	ptrel Start;
	ptrel ptr;
	ptrel End;
} DList;

// Инициализация списка
void InitDList(DList *D);

// Вставка в список до указателя
void PutDListL(DList *D, BaseType E);

// Вставка в список после указателя
void PutDListR(DList *D, BaseType E);

// Взятие из списка до указателя
void GetDListL(DList *D, BaseType *E);

// Взятие из списка после указателя
void GetDListR(DList *D, BaseType *E);

// Чтение из списка
void ReadDList(DList *D, BaseType *E);

// Проверка на пустоту
bool EmptyDList(DList *D);

// Подсчет количества элементов в списке
unsigned int Count(DList *D);

// Перемещение текущего указателя в начало списка
void BeginPtr(DList *D);

// Перемещение текущего указателя в конец списка
void EndPtr(DList *D);

// Перемещение текущего указателя на один элемент вперед
void MoveForward(DList *D);

// Перемещение текущего указателя на один элемент назад
void MoveBack(DList *D);

// Перемещение текущего указателя на указанную позицию
void MoveTo(DList *D, unsigned int n);

// Очистка списка
void ClearDList(DList *D);

// Копирование первого списка во второй
void CopyDList(DList *D1, DList *D2);

#endif