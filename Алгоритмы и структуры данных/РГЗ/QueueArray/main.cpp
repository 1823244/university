#include "QueueArray.h"

int main(int argc, char const *argv[])
{
	Queue Q;
	int x;
	InitQueue(&Q);
	x = 1;
	PutQueue(&Q, x, 1);
	x = 2;
	PutQueue(&Q, x, 2);
	x = 5;
	PutQueue(&Q, x, 100);
	GetQueue(&Q, &x);
	printf("%d\n", x);
	GetQueue(&Q, &x);
	printf("%d\n", x);
	GetQueue(&Q, &x);
	printf("%d\n", x);
	x = 0;
	GetQueue(&Q, &x);
	printf("%d\n", x);
	return 0;
}