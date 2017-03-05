#ifndef QUEUE_LIST_H
#define QUEUE_LIST_H

#include "SLL.h"

const short QueueOk = ListOk;
const short QueueEmpty = ListEmpty;
const short QueueNotMem = ListNotMem;
static short QueueError;

typedef List Queue;

// Инициализация очереди
void InitQueue(Queue *Q);

// Вставка в очередь
void PutQueue(Queue *Q, BaseType E);

// Взятие из очереди
void GetQueue(Queue *Q, BaseType *E);

// Чтение из очереди
void ReadQueue(Queue *Q, BaseType *E);

// Вывод содержимого очереди
void PrintQueue(Queue *Q);

// Очистка очереди
void DoneQueue(Queue *Q);

// Проверка на пустоту
bool EmptyQueue(Queue *Q);

#endif