#ifndef UNSORTED_TABLE_LIST_H
#define UNSORTED_TABLE_LIST_H

#include <stdio.h>
#include <stdlib.h>
#include "SLL.h"

const short TableOk = ListOk;
const short TableEmpty = ListEmpty;
const short TableNotMem = ListNotMem;
const short TableEnd = ListEnd;
const short TableNotContent = 10;
const short TableAlreadyContent = 11;
static short TableError = ListError;

typedef List Table;

// Инициализация таблицы
void InitTable(Table *T);

// Вставка в таблицу
void PutTable(Table *T, DataType E, KeyType K);

// Взятие из таблицы
void GetTable(Table *T, DataType *E, KeyType K);

// Чтение из таблицы
void ReadTable(Table *T, DataType *E, KeyType K);

// Проверка на пустоту
bool EmptyTable(Table *T);

// Очистка таблицы
void DoneTable(Table *T);

#endif