#include "SLL.h"

// Инициализация списка
void InitList(List *L) {
	L->Start = (ptrel)malloc(sizeof(element));

	if (L->Start == NULL) {
		ListError = ListNotMem;
		return;
	} 

	L->ptr = L->Start;
	L->ptr->next = NULL;
	ListError = ListOk;
}

// Вставка в список
void PutList(List *L, BaseType E) {
	ptrel tmp = (ptrel)malloc(sizeof(element));

	if (tmp == NULL) {
		ListError = ListNotMem;
		return;
	} 

	tmp->data = E;
	tmp->next = L->ptr->next;
	L->ptr->next = tmp;	
	L->ptr = L->ptr->next;
}

// Взятие из списка
void GetList(List *L, BaseType *E) {
	ptrel tmp = L->ptr;

	if (EmptyList(L)) {
		return;
	}
	
	L->ptr = L->Start;
	while (L->ptr->next != tmp) {
		MovePtr(L);
	}
	*E = L->ptr->next->data;
	L->ptr->next = L->ptr->next->next;
	free(tmp);
}

// Чтение из списка
void ReadList(List *L, BaseType *E) {
	if (EmptyList(L)) {
		return;
	}
	*E = L->ptr->data;
}

// Проверка на пустоту
bool EmptyList(List *L) {
	if (L->Start->next == NULL) {
		ListError = ListEmpty;
		return true;
	}
	return false;
}

// Нахождение количества элементов в списке
unsigned int Count(List *L) {
	unsigned int counter = 0;
	ptrel tmp = L->Start;

	while (tmp->next != NULL) {
		counter++;
		tmp = tmp->next;
	}

	return counter;
}

// Перемещение текущего указателя в начало списка
void BeginPtr(List *L) {
	L->ptr = L->Start;
}

// Перемещение текущего указателя в конец списка
void EndPtr(List *L) {
	if(EmptyList(L)) {
		return;
	}
	L->ptr = L->Start;
	while (L->ptr->next != NULL) {
		L->ptr = L->ptr->next;
	}
}

// Перемещение текущего указателя на один элемент вперед
void MovePtr(List *L) {
	if (L->ptr->next == NULL) {
		ListError = ListEnd;
		return;
	}
	L->ptr = L->ptr->next;
}

// Перемещение текущего указателя на указанную позицию
void MoveTo(List *L, unsigned int n) {
	unsigned int counter = 0;
	ptrel tmp = L->Start;

	while (counter++ != n) {
		tmp = tmp->next;
		if (tmp == NULL) {
			ListError = ListEnd;
			return;
		}
	}
	L->ptr = tmp;
}

// Очистка списка
void ClearList(List *L) {
	ptrel tmp, tmpnext;
	tmp = L->Start;
	tmpnext = tmp->next;

	while (EmptyList(L)) {
		tmp = tmpnext;
		tmpnext = tmpnext->next;
		free(tmp);
	}

	free(L->Start);
	ListError = ListEmpty;
}

// Копирование первого списка во второй
void CopyList(List *L1, List *L2) {
	ptrel tmp = L1->Start;

	while(tmp->next != NULL) {
		PutList(L2, tmp->next->data);
		tmp = tmp->next;
	}
}