#include "QueueArray.h"

// Инициализация очереди
void InitQueue(Queue *Q) {
	Q->Top = Q->Bottom = 0;
	QueueError = QueueOk;
}

// Вставка в очередь
void PutQueue(Queue *Q, BaseType E, int P) {
	base tmp;
	tmp.data = E;
	tmp.priority = P;

	if (FullQueue(Q)) {
		return;
	}
	Q->Buf[Q->Bottom] = tmp;
	Q->Bottom = (Q->Bottom + 1) % QueueSize;
}

// Взятие из очереди
void GetQueue(Queue *Q, BaseType *E) {
	int i = Q->Top;
	int maxPriority = Q->Buf[Q->Top].priority, maxIndex = i;;
	if (EmptyQueue(Q)) {
		return;
	}
	while (i != Q->Bottom) {
		i = (i + 1) % QueueSize;
		if (Q->Buf[i].priority > maxPriority) {
			maxPriority = Q->Buf[i].priority;
			maxIndex = i;
		}
	}

	*E = Q->Buf[maxIndex].data;
	while (maxIndex != Q->Top) {
		int newIndex;
		newIndex = (maxIndex - 1 + QueueSize) % QueueSize;
		Q->Buf[maxIndex] = Q->Buf[newIndex];
		maxIndex = newIndex;
	}
	Q->Top = (Q->Top + 1) % QueueSize;
}

// Проверка на заполненность
bool FullQueue(Queue *Q) {
	if (((Q->Bottom + 1) % QueueSize) == Q->Top) {
		QueueError = QueueFull;
		return true;
	}
	return false;
}

// Проверка на пустоту
bool EmptyQueue(Queue *Q) {
	if (Q->Top == Q->Bottom) {
		QueueError = QueueEmpty;
		return true;
	}
	return false;
}