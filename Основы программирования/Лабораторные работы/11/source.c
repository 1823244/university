#include <stdio.h>
#include <math.h>
#include <locale.h>

int main(void) {

	int min1 = 99, max1 = 0, min2 = 99, max2 = 0;		
	double factor1[100], factor2[100];

	setlocale(LC_ALL, "rus");

	printf("Задание:\n\tНайти остаток от деления многочлена Pn(x) на многочлен Qm(x)\n");
	printf("Введите Pn(x): \n\t");
	enterPolynomial(factor1, &min1, &max1);
	printf("Введите Qm(x): \n\t");
	enterPolynomial(factor2, &min2, &max2);
	
	divisionPolynomial(factor1, min1, &max1, factor2, min2, max2);
	printf("Остаток от делёения Pn(x) на Qm(x): \n\t");
	printPolynomial(factor1, min1, max1);

	return 0
} 

void enterPolynomial(double *factor, int *min, int *max) {
	
	int sign = 1, power_sign = 1, coefficient = 0, power = 0, option = 0, i = 0;
	char c, previous = '!';

	*min = 99;
	*max = 0;

	for (; i < 100; i++) {factor[i] = 0;}

	while ((c = getchar()) != '\n') {
		
		if (c == 'x') {
			if (coefficient == 0) {
				coefficient = 1;
			}

			coefficient *= sign;
			option = 1;
		}

		if (c == '-' && previous == '^') {
			power_sign = -1;
		}

		if ((c == '-' || c == '+' ) && (previous != '^')) {
			switch (c) {
				case '-': 
					sign = -1;
					break;
				case '+': 
					sign = 1;
					break;
			}
			option = 0;
			if (power == 0 && previous == 'x') {
				power = 1;
			}
			power *= power_sign;
			power += 50;

			if (*min > power) {
				*min = power; 
			} 
			if (*max < power) {
				*max = power;
			}

			factor[power] = coefficient;
			coefficient = 0;
			power = 0;
			power_sign = 1;
		}

		if (c >= '0' && c <= '9') {
			if (option == 0) {
				coefficient *= 10;
				coefficient += c -'0';
			} else {
				power *= 10;
				power += c - '0';
			}
		}

		if (c != ' ') {
			previous = c;
		}

	}

	if (previous != 'x' && power == 0) {
		coefficient *= sign;
	}
	if (previous == 'x' && power == 0) {
		power = 1;
	}

	power = power_sign*power + 50;

	if (*min > power) {
		*min = power; 
	} 
	if (*max < power) {
		*max = power;
	}

	factor[power] = coefficient; 
}

void printPolynomial(double *factor, int min, int max) {
	
	int coefficient, power, option = 0;

	for (; max >= min; max--) {
		if (factor[max] != 0) {
			if (option != 0) {
				if (factor[max] > 0) {
					printf(" + ");
				} else {
					printf(" - ");
				}
			} else if (option == 0 && factor[max] < 0) {
				printf("-");
			}
			option = 1;
			
			coefficient = fabs(factor[max]);
			power = max - 50;
			
			if (power == 0) {
				printf("%d", coefficient);
			}
			
			if (power != 0) {
				if (coefficient != 1) {
					printf("%d", coefficient);
				}
				printf("%c", 'x');
				
				if (power != 1) {
					printf("^%d", power);
				}
			}
		}
	}

	getchar();
}

int divisionPolynomial(double *factor1, int min1, int *max1, double *factor2, int min2, int max2) {
	
	int difference, tmp_min, i;
	double coefficient;
	double tmp[100];

	if (*max1 < max2) {
		return 0;
	}

	for (i = 0; i < 100; i++) {tmp[i] = 0;}

	while (*max1 >= max2) {
		difference = *max1 - max2;
		coefficient = factor1[*max1] / factor2[max2];
		tmp_min = min2;
		for (; tmp_min <= max2; tmp_min++) {
			if (factor2[tmp_min] != 0) {
				tmp[tmp_min + difference] = factor2[tmp_min]*coefficient;
			}
		}

		tmp_min = min1 + difference;
		for (; tmp_min <= *max1; tmp_min++) {
			if (tmp[tmp_min] != 0) {
				factor1[tmp_min] -= tmp[tmp_min];
				if (factor1[tmp_min] < 0.001 && factor1[tmp_min] > 0) {
					factor1[tmp_min] = 0;
				}
			}
		}

		*max1 -= 1;
	}

	return 0;
}