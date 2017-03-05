#ifndef SLL_H
#define SLL_H

#include <stdio.h>
#include <stdlib.h>

const short ListOk = 0;
const short ListNotMem = 1;
const short ListEmpty = 2;
const short ListEnd = 3;
static short ListError;

typedef int BaseType;
typedef struct element * ptrel;
typedef struct element {
	BaseType data;
	ptrel next;
} element;
typedef struct {
	ptrel Start;
	ptrel ptr;
} List;

// Инициализация списка
void InitList(List *L);

// Вставка в список
void PutList(List *L, BaseType E);

// Взятие из списка
void GetList(List *L, BaseType *E);

// Чтение из списка
void ReadList(List *L, BaseType *E);

// Проверка на пустоту
bool EmptyList(List *L);

// Нахождение количества элементов в списке
unsigned int Count(List *L);

// Перемещение текущего указателя в начало списка
void BeginPtr(List *L);

// Перемещение текущего указателя в конец списка
void EndPtr(List *L);

// Перемещение текущего указателя на один элемент вперед
void MovePtr(List *L);

// Перемещение текущего указателя на заданную позицию
void MoveTo(List *L, unsigned int n);

// Очистка списка
void ClearList(List *L);

// Копирование первого списка во второй
void CopyList(List *L1, List *L2);

#endif