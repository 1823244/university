#ifndef QUEUE_LIST_H
#define QUEUE_LIST_H

#include "SLL.h"

const short QueueOk = ListOk;
const short QueueEmpty = ListEmpty;
const short QueueNotMem = ListNotMem;
static short QueueError;

typedef List Queue;

// ������������� �������
void InitQueue(Queue *Q);

// ������� � �������
void PutQueue(Queue *Q, BaseType E);

// ������ �� �������
void GetQueue(Queue *Q, BaseType *E);

// ������ �� �������
void ReadQueue(Queue *Q, BaseType *E);

// ����� ����������� �������
void PrintQueue(Queue *Q);

// ������� �������
void DoneQueue(Queue *Q);

// �������� �� �������
bool EmptyQueue(Queue *Q);

#endif