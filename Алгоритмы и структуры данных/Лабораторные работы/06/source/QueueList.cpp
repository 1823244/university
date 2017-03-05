#include "QueueList.h"

// Инициализация очереди
void InitQueue(Queue *Q) {
	InitList(Q);
	QueueError = GetListError();
}

// Вставка в очередь
void PutQueue(Queue *Q, BaseType E) {
	BeginPtr(Q);
	PutList(Q, E);
	QueueError = GetListError();
}

// Взятие из очереди
void GetQueue(Queue *Q, BaseType *E) {
	EndPtr(Q);
	GetList(Q, E);
	QueueError = GetListError();
}

// Чтение из очереди
void ReadQueue(Queue *Q, BaseType *E) {
	ReadList(Q, E);
	QueueError = GetListError();
}

// Вывод содержимого очереди
void PrintQueue(Queue *S) {
	BaseType e;
	Queue tmpQueue;

	InitQueue(&tmpQueue);

	while(!EmptyQueue(S)) {
		GetQueue(S, &e);
		PutQueue(&tmpQueue, e);
	}

	while(!EmptyQueue(&tmpQueue)) {
		GetQueue(&tmpQueue, &e);
		printf("\t--(%s, %d, %d)\n", e.Name, e.Time, e.P);
		PutQueue(S, e);
	}
}

// Очистка очереди
void DoneQueue(Queue *Q) {
	ClearList(Q);
	QueueError = GetListError();
}

// Проверка на пустоту
bool EmptyQueue(Queue *Q) {
	return EmptyList(Q);
}