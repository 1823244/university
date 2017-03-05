#ifndef LIST_H
#define LIST_H

#include <stdio.h>
#include <stdlib.h>

const short ListOk = 0;
const short ListNotMem = 1;
const short ListEmpty = 2;
const short ListFull = 3;

typedef struct {
	int power;
	int value;
} basetype;
typedef basetype base;
typedef struct element * ptrel;
typedef struct element {
	base data;
	ptrel next;
} element;
typedef struct {
	ptrel Start;
	ptrel ptr;
	unsigned int N;
} List;

static short ListError;

void InitList(List *L, unsigned int N);
void PutList(List *L, base E);
void GetList(List *L, base *E);
void ReadList(List *L, base *E);
int EmptyList(List *L);
int FullList(List *L);
unsigned int Count(List *L);
void BeginPtr(List *L);
void EndPtr(List *L);
void MovePtr(List *L);
void MoveTo(List *L, unsigned int n);
void ClearList(List *L);
void CopyList(List *L1, List *L2);

#endif