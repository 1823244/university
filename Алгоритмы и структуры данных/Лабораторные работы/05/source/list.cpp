#include "list.h"

void InitList(List *L, unsigned int N) {
	L->Start = (ptrel)malloc(sizeof(element));

	if (L->Start == NULL) {
		ListError = ListNotMem;
		return;
	} 

	L->ptr = L->Start;
	L->ptr->next = NULL;
	L->N = N;
	ListError = ListOk;
}

void PutList(List *L, base E) {
	ptrel tmp = (ptrel)malloc(sizeof(element));

	if (tmp == NULL) {
		ListError = ListNotMem;
		return;
	} 
	if (Count(L) == L->N) {
		ListError = ListFull;
		free(tmp);
		return;
	}
	tmp->data = E;
	tmp->next = L->ptr->next;
	L->ptr->next = tmp;
	
	L->ptr = L->ptr->next;
}

void GetList(List *L, base *E) {
	ptrel tmp = L->ptr, tmpnext;

	if (L->Start == NULL) {
		ListError = ListEmpty;
		return;
	}
	tmpnext = L->ptr->next->next;
	*E = L->ptr->next->data;
	free(L->ptr->next);
	L->ptr->next = tmpnext;
	L->ptr = tmp;
}

void ReadList(List *L, base *E) {
	if (L->Start == NULL) {
		ListError = ListEmpty;
		return;
	}
	*E = L->ptr->data;
}

int EmptyList(List *L) {
	return (L->Start == NULL) ? 1 : 0;
}

int FullList(List *L) {
	return (Count(L) == L->N) ? 1 : 0;
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
	if(L->ptr->next == NULL) {
		ListError = ListEmpty;
		return;
	}
	while (L->ptr->next != NULL) {
		L->ptr = L->ptr->next;
	}
}

void MovePtr(List *L) {
	if (L->ptr->next == NULL) {
		ListError = ListFull;
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
			ListError = ListFull;
			return;
		}
	}
	L->ptr = tmp;
}

void ClearList(List *L) {
	ptrel tmp, tmpnext;
	tmp = L->Start;
	tmpnext = tmp->next;

	while (tmpnext != NULL) {
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
		if (FullList(L2)) {
			ListError = ListFull;
			return;
		}
	}
}