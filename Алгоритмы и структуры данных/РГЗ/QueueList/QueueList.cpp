#include "QueueList.h"

// Инициализация очереди
void InitQueue(Queue *Q) {
	InitList(Q);
	QueueError = ListError;
}

// Вставка в очередь
void PutQueue(Queue *Q, BaseType E) {
	BeginPtr(Q);
	PutList(Q, E);
	QueueError = ListError;
}

// Взятие из очереди
void GetQueue(Queue *Q, BaseType *E) {
	EndPtr(Q);
	GetList(Q, E);
	QueueError = ListError;
}

// Очистка очереди
void DoneQueue(Queue *Q) {
	ClearList(Q);
	QueueError = ListError;
}

// Проверка на пустоту
bool EmptyQueue(Queue *Q) {
	return EmptyList(Q);
}