#include "DLL.h"

// Инициализация списка
void InitDList(DList *D) {
	D->Start = (ptrel)malloc(sizeof(element));
	D->End = (ptrel)malloc(sizeof(element));

	if ((D->Start == NULL) || (D->End == NULL)) {
		DListError = DListNotMem;
		free(D->Start);
		free(D->End);
		return;
	} 

	D->Start->next = D->End;
	D->Start->prev = NULL;
	D->End->prev = D->Start;
	D->End->next = NULL;
	D->ptr = D->Start;

	DListError = DListOk;
}

// Вставка в список до указателя
void PutDListL(DList *D, BaseType E) {
	if (D->ptr != D->Start) {
		ptrel tmp = (ptrel)malloc(sizeof(element));

		if (tmp == NULL) {
			DListError = DListNotMem;
			return;
		} 

		tmp->data = E;
		tmp->next = D->ptr;
		tmp->prev = D->ptr->prev;

		D->ptr->prev->next = tmp;	
		D->ptr->prev = tmp;
	}
}

// Вставка в список после указателя
void PutDListR(DList *D, BaseType E) {
	if (D->ptr != D->End) {
		ptrel tmp = (ptrel)malloc(sizeof(element));

		if (tmp == NULL) {
			DListError = DListNotMem;
			return;
		} 

		tmp->data = E;
		tmp->next = D->ptr->next;
		tmp->prev = D->ptr;

		D->ptr->next->prev = tmp;	
		D->ptr->next = tmp;
	}
}

// Взятие из списка до указателя
void GetDListL(DList *D, BaseType *E) {
	if (EmptyDList(D)) {
		return;
	}

	if (D->ptr != D->Start) {
		ptrel tmp = D->ptr->prev;
		*E = tmp->data;
		D->ptr->prev = tmp->prev;
		tmp->prev->next = D->ptr;
		free(tmp);
	}
}

// Взятие из списка после указателя
void GetDListR(DList *D, BaseType *E) {
	if (EmptyDList(D)) {
		return;
	}

	if (D->ptr != D->End) {
		ptrel tmp = D->ptr->next;
		*E = tmp->data;
		D->ptr->next = tmp->next;
		tmp->next->prev = D->ptr;
		free(tmp);
	}
}

// Чтение из списка
void ReadDList(DList *D, BaseType *E) {
	if (EmptyDList(D)) {
		return;
	}
	*E = D->ptr->data;
}

// Проверка на пустоту
bool EmptyDList(DList *D) {
	if (D->Start == D->End) {
		DListError = DListEmpty;
		return true;
	}
	return false;
}

// Подсчет количества элементов в списке
unsigned int Count(DList *D) {
	unsigned int counter = 0;
	ptrel tmp = D->Start;

	while (tmp->next != D->End) {
		counter++;
		tmp = tmp->next;
	}

	return counter;
}

// Перемещение текущего указателя в начало списка
void BeginPtr(DList *D) {
	D->ptr = D->Start;
}

// Перемещение текущего указателя в конец списка
void EndPtr(DList *D) {
	D->ptr = D->End;
}

// Перемещение текущего указателя на один элемент вперед
void MoveForward(DList *D) {
	if (D->ptr != D->End) {
		DListError = DListEnd;
		return;
	}
	D->ptr = D->ptr->next;
}

// Перемещение текущего указателя на один элемент назад
void MoveBack(DList *D) {
	if (D->ptr != D->Start) {
		DListError = DListEnd;
		return;
	}
	D->ptr = D->ptr->prev;
}

// Перемещение текущего указателя на указанную позицию
void MoveTo(DList *D, unsigned int n) {
	unsigned int counter = 0;
		ptrel tmp = D->Start;

	while (counter++ != n) {
		tmp = tmp->next;
		if (tmp == NULL) {
			DListError = DListEnd;
			return;
		}
	}
	D->ptr = tmp;
}

// Очистка списка
void ClearDList(DList *D) {
	ptrel tmp, tmpnext;
	tmp = D->Start;
	tmpnext = tmp->next;

	while (!EmptyDList(D)) {
		tmp = tmpnext;
		tmpnext = tmpnext->next;
		free(tmp);
	}

	free(D->Start);
	free(D->End);
	DListError = DListEmpty;
}

// Копирование первого списка во второй
void CopyDList(DList *D1, DList *D2) {
	ptrel tmp = D1->Start;

	while(tmp->next != NULL) {
		PutDList(D2, tmp->next->data);
		tmp = tmp->next;
	}
}