#ifndef SLL_H
#define SLL_H

#include "SLL.h"

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

void GetList(List *L, BaseType *E) {
	ptrel tmp = L->ptr;

	if (EmptyList(L)) {
		return;
	}
	if (L->ptr != L->Start) {
		L->ptr = L->Start;
		while (L->ptr->next != tmp) {
			MovePtr(L);
		}
	} else {
		tmp = tmp->next;
	}
	*E = L->ptr->next->data;
	L->ptr->next = L->ptr->next->next;
	free(tmp);
}

void ReadList(List *L, BaseType *E) {
	if (EmptyList(L)) {
		return;
	}
	*E = L->ptr->data;
}

bool EmptyList(List *L) {
	if (L->Start->next == NULL) {
		ListError = ListEmpty;
		return true;
	}
	return false;
}

unsigned int Count(List *L) {
	unsigned int counter = 0;
	ptrel tmp = L->Start;

	while (tmp->next != NULL) {
		counter++;
		tmp = tmp->next;
	}

	return counter;
}

void BeginPtr(List *L) {
	L->ptr = L->Start;
}

void EndPtr(List *L) {
	L->ptr = L->Start;
	if(EmptyList(L)) {
		return;
	}
	while (L->ptr->next != NULL) {
		L->ptr = L->ptr->next;
	}
}

void MovePtr(List *L) {
	if (L->ptr->next == NULL) {
		ListError = ListEnd;
		return;
	}
	L->ptr = L->ptr->next;
}

void MoveTo(List *L, unsigned int n) {
	unsigned int counter = 1;
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

void CopyList(List *L1, List *L2) {
	ptrel tmp = L1->Start;

	while(tmp->next != NULL) {
		PutList(L2, tmp->next->data);
		tmp = tmp->next;
	}
}

int GetListError() {
	return ListError;
}

#endif