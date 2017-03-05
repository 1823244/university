#include "list.h"

bool isEqual(List *L1, List *L2);
void input(List *L);

int main() {
	List l, l2;
	base x;
	InitList(&l, 2);
	x.value = 40;
	x.power = 1;
	PutList(&l, x);
	
	x.value = 0;
	EndPtr(&l);
	ReadList(&l, &x);
	printf("Read: %d\n", x.value);

	x.value = 0;
	EndPtr(&l);
	GetList(&l, &x);
	printf("Get:  %d\n", x.value);
	// input(&l);
	// input(&l2);
	// if (isEqual(&l, &l2)) {
	// 	printf("%s\n", "true");
	// } else {
	// 	printf("%s\n", "false");
	// }

	return 0;
}

bool isEqual(List *L1, List *L2) {
	ptrel tmpl1 = L1->ptr, tmpl2 = L2->ptr; 

	BeginPtr(L1);
	BeginPtr(L2);

	while ((L1->ptr->next != NULL) && (L2->ptr->next !=NULL)) {
		if ((L1->ptr->data.power != L2->ptr->data.power) || (L1->ptr->data.value != L2->ptr->data.value)) {
			L1->ptr = tmpl1;
			L2->ptr = tmpl2;
			return false;
		}
		L1->ptr = L1->ptr->next;
		L2->ptr = L2->ptr->next;
	}

	if ((L1->ptr->next != NULL) || (L2->ptr->next !=NULL))
	{
		L1->ptr = tmpl1;
		L2->ptr = tmpl2;
		return false;
	}

	return true;
}

void input(List *L) {
	int power, value;
	base e;
	printf("%s\n", "Enter the power and coefficient, separated by space.\nEnter -1 for exit.");

	scanf("%d", &e.power);
	InitList(L, 300);
	while (e.power > -1) {
		scanf("%d", &e.value);
		if (e.value != 0) {
			PutList(L, e);
		}
		scanf("%d", &e.power);
	}
	EndPtr(L);
}