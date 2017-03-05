#include "UnsortedTableArray.h"

int main(int argc, char const *argv[])
{
	Table T;
	int data;

	InitTable(&T);

	PutTable(&T, 1, 1);
	PutTable(&T, 2, 2);
	PutTable(&T, 3, 3);
	PutTable(&T, 4, 4);
	PutTable(&T, 4, 4);
	ReadTable(&T, &data, 2);
	printf("%d\n", data);
	GetTable(&T, &data, 3);
	printf("%d\n", data);
	ReadTable(&T, &data, 3);
	printf("%d\n", data);
	GetTable(&T, &data, 2);
	printf("%d\n", data);
	GetTable(&T, &data, 1);
	printf("%d\n", data);
	GetTable(&T, &data, 4);
	printf("%d\n", data);
	GetTable(&T, &data, 1);
	printf("%d\n", data);
	printf("%d\n", EmptyTable(&T));
	return 0;
}