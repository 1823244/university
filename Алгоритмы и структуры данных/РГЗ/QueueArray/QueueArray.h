#ifndef QUEUE_ARRAY_H
#define QUEUE_ARRAY_H

#include <stdio.h>
#include <stdlib.h>

const short QueueSize = 1000;
const short QueueOk = 0;
const short QueueEmpty = 1;
const short QueueFull = 2;
static short QueueError;

typedef int BaseType;
typedef struct base {
	BaseType data;
	int priority;
} base;
typedef struct {
	base Buf[QueueSize];
	unsigned Top;
	unsigned Bottom;
} Queue;

// Инициализация очереди
void InitQueue(Queue *Q);

// Вставка в очередь
void PutQueue(Queue *Q, BaseType E, int P);

// Взятие из очереди
void GetQueue(Queue *Q, BaseType *E);

// Проверка на заполненность
bool FullQueue(Queue *Q);

// Проверка на пустоту
bool EmptyQueue(Queue *Q);