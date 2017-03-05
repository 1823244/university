#include "UnsortedTableList.h"

int main(int argc, char const *argv[])
{
	Table T;
	int data;

	InitTable(&T);
	PutTable(&T, 2, 2);
	PutTable(&T, 3, 3);
	GetTable(&T, &data, 2);
	printf("%d\n", data);
	
	return 0;
}