#ifndef UNSORTED_TABLE_ARRAY_H
#define UNSORTED_TABLE_ARRAY_H

#include <stdio.h>
#include <stdlib.h>

const short TableSize = 1000;
const short TableOk = 0;
const short TableEmpty = 1;
const short TableFull = 2;
const short TableNotContent = 3;
const short TableAlreadyContent = 4;
static short TableError;

typedef int DataType;
typedef char * KeyType;
typedef struct base {
	DataType data;
	KeyType key;
} base;

typedef struct {
	base Buf[TableSize];
	unsigned Top;
} Table;

// ������������� �������
void InitTable(Table *T);

// ������� � �������
void PutTable(Table *T, DataType E, KeyType K);

// ������ �� �������
void GetTable(Table *T, DataType *E, KeyType K);

// ������ �� �������
void ReadTable(Table *T, DataType *E, KeyType K);

// �������� �� �������
bool EmptyTable(Table *T);

// �������� �� �������������
bool FullTable(Table *T);

// ����������� ���� ������
short GetTableError();

#endif