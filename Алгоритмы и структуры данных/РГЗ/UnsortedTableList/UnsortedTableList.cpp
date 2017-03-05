#include "UnsortedTableList.h"

// Инициализация таблицы
void InitTable(Table *T) {
	InitList(T);
	TableError = ListError;
}

// Вставка в таблицу
void PutTable(Table *T, DataType E, KeyType K) {
	BaseType tmp, tmp2;

	tmp.data = E;
	tmp.key = K;
	BeginPtr(T);
	while (TableError != TableEnd) {
		MovePtr(T);
		ReadList(T, &tmp2);
		if (tmp2.key == tmp.key) {
			TableError = TableAlreadyContent;
			return;
		}
		TableError = ListError;
	}
	EndPtr(T);
	PutList(T, tmp);
	TableError = ListError;
}

// Взятие из таблицы
void GetTable(Table *T, DataType *E, KeyType K) {
	BaseType tmp;

	TableError = ListError;
	BeginPtr(T);
	while (TableError != TableEnd) {
		MovePtr(T);
		ReadList(T, &tmp);
		if (tmp.key == K) {
			GetList(T, &tmp);
			*E = tmp.data;
			return;
		}
		TableError = ListError;
	}
	TableError = TableNotContent;
}

// Чтение из таблицы
void ReadTable(Table *T, DataType *E, KeyType K) {
	BaseType tmp;

	TableError = ListError;
	BeginPtr(T);
	while (TableError != TableEnd) {
		MovePtr(T);
		ReadList(T, &tmp);
		if (tmp.key == K) {
			*E = tmp.data;
			return;
		}
		TableError = ListError;
	}
	TableError = TableNotContent;
}

// Проверка на пустоту
bool EmptyTable(Table *T) {
	return EmptyList(T);
}

// Очистка таблицы
void DoneTable(Table *T) {
	ClearList(T);
}