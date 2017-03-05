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
	printf("%s", "Задание: Описать функцию для ввода с терминала вещественного числав форме\n\t с фиксированной точкой. С помощью этой функции ввести n чисел\n\t затем вывести наибольшее из введенных\n\nВведите количество вводимых чисел: ");
	
	scanf("%u%*c", &n);

	if (n == 0) {
		printf("%s", "Увы, но максимальных в таком случае не найти.\n");
	} else {
		printf("%s", "Введите числа:\n\t1: ");
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

		printf("\n%s %.*f\n", "Максимальное:", order_max, max);
	}
	getchar();
}