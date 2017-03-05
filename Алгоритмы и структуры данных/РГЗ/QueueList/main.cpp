#include "QueueList.h"

int main(int argc, char const *argv[])
{
	Queue Q;
	int x = 0;
	InitQueue(&Q);
	PutQueue(&Q, 1);
	PutQueue(&Q, 2);
	PutQueue(&Q, 3);
	GetQueue(&Q, &x);
	printf("%d\n", x);
	GetQueue(&Q, &x);
	printf("%d\n", x);
	GetQueue(&Q, &x);
	printf("%d\n", x);
	return 0;
}