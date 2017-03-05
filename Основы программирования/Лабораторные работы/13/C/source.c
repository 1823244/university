#include <stdio.h>
#include <locale.h>

double enterDouble(int *order) {
	char x;
	int integer = 0, sign = 1, i;
	double fraction = 0, tmp;

	*order = 1;

	if ((x = getchar()) == '-') {
		sign = -1;
	} else {
		integer += (int) (x - '0');
	}
	while ((x = getchar()) != '.') {
		integer *= 10;
		integer += (int) (x - '0');
	}
	x = getchar();
	do {
		tmp = (float) (x - '0');
		for (i = 0; i < *order; i++) {
			tmp /= 10;
		}	
		(*order)++;
		fraction += tmp;
		x = getchar();
	} while (x != '\n');
	return sign * (integer + fraction);
}
int main(void) {
	int order, order_max;
	double x, max;
	unsigned n = 0, i = 2;

	setlocale(LC_ALL, "RUS");
	printf("%s", "�������: ������� ������� ��� ����� � ��������� ������������� ������ �����\n\t � ������������� ������. � ������� ���� ������� ������ n �����\n\t ����� ������� ���������� �� ���������\n\n������� ���������� �������� �����: ");
	
	scanf("%u%*c", &n);

	if (n == 0) {
		printf("%s", "���, �� ������������ � ����� ������ �� �����.\n");
	} else {
		printf("%s", "������� �����:\n\t1: ");
		max = enterDouble(&order);
		order_max = order - 1;
		for	(; i <= n; i++) {
			printf("\t%u: ", i);
			x = enterDouble(&order);
			if (max < x) {
				max = x;
				order_max = order - 1;
			}
		}

		printf("\n%s %.*f\n", "������������:", order_max, max);
	}
	getchar();
}