#include <stdio.h>
#include <stdlib.h>
#include "TInquiry.h"

const short ListOk = 0;
const short ListNotMem = 1;
const short ListEmpty = 2;
const short ListEnd = 3;
static short ListError;

typedef TInquiry BaseType;

typedef struct element * ptrel;
typedef struct element {
	BaseType data;
	ptrel next;
} element;
typedef struct {
	ptrel Start;
	ptrel ptr;
} List;

void InitList(List *L);
void PutList(List *L, BaseType E);
void GetList(List *L, BaseType *E);
void ReadList(List *L, BaseType *E);
bool EmptyList(List *L);
unsigned int Count(List *L);
void BeginPtr(List *L);
void EndPtr(List *L);
void MovePtr(List *L);
void MoveTo(List *L, unsigned int n);
void ClearList(List *L);
void CopyList(List *L1, List *L2);
int GetListError();