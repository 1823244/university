#include "SLL.h"

int main() {
	List L;
	int x = 0;
	InitList(&L);
	BeginPtr(&L);
	PutList(&L, 1);
	PutList(&L, 2);
	MoveTo(&L, 2);
	ReadList(&L, &x);
	printf("%d\n", x);
	return 0;
}