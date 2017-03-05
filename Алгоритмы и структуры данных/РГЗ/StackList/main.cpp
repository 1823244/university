#include "StackList.h"

int main() {
	Stack S;
	int x = 1;

	InitList(&S);
	PutStack(&S, x);
	x = 2;
	PutStack(&S, x);
	x = 3;
	PutStack(&S, x);
	x = 0;
	GetStack(&S, &x);
	printf("%d\n", x);
	GetStack(&S, &x);
	printf("%d\n", x);
	GetStack(&S, &x);
	printf("%d\n", x);
	x = 0;
	GetStack(&S, &x);
	printf("%d\n", x);
	return 0;
}