#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

#define n 4

typedef int matrix1[n][n];

typedef int row[n];
typedef row *matrix2;

typedef int (*matrix3)[n];

typedef int **matrix4;

void find_sum(char);
void summator_a(void);
void summator_b(void);
void summator_c(void);
void summator_d(int);
void input_a(matrix1);
void input_b(matrix2);
void input_c(matrix3);
void input_d(matrix4, int);
void output_a(matrix1);
void output_b(matrix2);
void output_c(matrix3);
void output_d(matrix4, int);

matrix3 *getmem(int );

int main(void){
	char option;

	setlocale(LC_ALL, "RUS");

	printf("Задание: Дана матрица, все элементы которой различны. Назовем псевдодиагональю\n\t множество элементов этой матрицы, лежащих на прямой,\n\t параллельной прямой, содержащей элементы a[i, n-i+1],\n\t где n — порядок матрицы.\n\t Найти сумму максимальных элементов псевдодиагоналей данной матрицы.\n\nКакой вариант будем решать? (a, b, c, d): ");
	
	option = getchar();
	if (option >= 'a' && option <= 'd') {
		find_sum(option);
	} else {
		printf("\n\nУвы, такого варианта не существует.");
	}

	getchar();
	getchar();

	return 0;
}

void find_sum(char option){
	int x = 0;

	switch(option) {
	case 'a':
		summator_a();
		break;
	case 'b':
		printf("\nВведите количество столбцов матрицы: ");
		scanf("%*d");
		summator_b();
		break;
	case 'c':
		printf("\nВведите количество строк матрицы: ");
		scanf("%*d");
		summator_c();
		break;
	case 'd':
		printf("\nВведите размеры матрицы (NxM): ");
		scanf("%dx%*d", &x);
		if (x != 0) {
			summator_d(x);
		} else {
			printf("%s", "Увы, вы ввели матрицу с нулевым количеством элементов.");
		}
		break;
	}
}

#define patternSummatorBody		do {if (x == 1) { \
										sum = 0; \
									} else {max1 = *p[0][0]; \
								max2 = *p[x-1][x-1]; \
								sum = max1 + max2;} \
								for(i = 1; i < x - 1; i++) { \
									max1 = *p[i][0]; \
									max2 = *p[x - 1 - i][x-1]; \
									for(j = i; j >= 0; j--) { \
										if(*p[i - j][j] > max1) { \
											max1 = *p[i - j][j]; \
										} \
										if(*p[x - j - 1][j + x - i - 1] > max2) { \
											max2 = *p[x - j - 1][j + x - i - 1]; \
										} \
									} \
									sum += (max1 + max2); \
								} \
								printf("\n\nСумма максимальных элементов псевдодиагоналей: %i\n",  sum); \
							} while (0)\
								
void summator_a(void) {
	int i, j, max1, max2, sum, x = n;
	matrix1 *p;

	p = (matrix1*) malloc (sizeof(matrix1));

	if (p) {
		input_a(p);
		output_a(p);
		patternSummatorBody;
		free(p);
	} else {
		printf("\nНедостаточно памяти.");
	}
}

void summator_b(void) {
	int k = 0, i, j, max1, max2, sum, x = n, flag = 1;
	matrix2 p[n];

	for (; (k < n) && flag; k++) {
		p[k] = (row *) malloc (n*sizeof(matrix2));
		if(!p[k]) { flag = 0; }
	}
	
	if (flag) {
		input_b(p);
		output_b(p);
		patternSummatorBody;
	}  else {
		printf("\nНедостаточно памяти.");
	}

	for(i = 0; i < k; i++) {
		free(p[i]);
	} 
}

void summator_c(void) {
	int k = 0, i, j, max1, max2, sum, x = n, flag = 1; 
	matrix3 p; 

	if (p = getmem(n)) {
		input_c(p);
		output_c(p);
	//	patternSummatorBody;
	}  else {
		printf("\nНедостаточно памяти.");
	}
}

void summator_d(int x) {
	int *p_tmp, i, j, max1, max2, sum;
	matrix4 p;

	p_tmp = (int (*)) malloc(x*x);
	p = (matrix4) malloc(x*sizeof(int (*)));
	if (p && p_tmp) {
		for (i = 0; i < x; i++) {
			p[i] = p_tmp + i*x;
		}	
		input_d(p, x);
		output_d(p, x);
		if (x == 1) { 
			sum = 0;
		} else {
			max1 = p[0][0]; 
			max2 = p[x-1][x-1]; 
			sum = max1 + max2;
		}
		
		for (i = 1; i < x - 1; i++) { 
			max1 = p[i][0];
			max2 = p[x - 1 - i][x-1];
			for(j = i; j >= 0; j--) {
				if(p[i - j][j] > max1) { 
					max1 = p[i - j][j]; 
				} 
				if (p[x - j - 1][j + x - i - 1] > max2) {
					max2 = p[x - j - 1][j + x - i - 1]; 
				} 
			} 

			sum += (max1 + max2); 
		} 
		printf("\n\nСумма максимальных элементов псевдодиагоналей: %i\n",  sum); 
		
		free(p_tmp);
	} else {
		printf("\nНедостаточно памяти.");
	}

}

#define patternInputBody printf("\nВведите матрицу размера %dx%d:\n", x, x); \
						for(i = 0; i < x; i++) { \
							printf("\t"); \
							for(j = 0; j < x; j++) { \
								scanf("%d", &p[i][j]); \
							} \
						} \

void input_a(matrix1 *p) {
	int i, j, x = n;
	patternInputBody
}

void input_b(matrix2 *p) {
	int i, j, x = n;
	patternInputBody
}

void input_c(matrix3 *p) {
	int i, j, x = n;
	patternInputBody
}

void input_d(matrix4 *p, int x) {
	int i, j;
	patternInputBody
}

#define patternOutputBody printf("\nВведенная матрица:"); \
						for(i = 0; i < x; i++) { \
							printf("\n\t"); \
							for(j = 0; j < x; j++) { \
								printf("%d ", *p[i][j]); \
							} \
						} \

void output_a(matrix1 *p) {
	int i, j, x = n;
	patternOutputBody
}

void output_b(matrix2 *p) {
	int i, j, x = n;
	patternOutputBody
}

void output_c(matrix3 *p) {
	int i, j, x = n;
	patternOutputBody
}

void output_d(matrix4 *p, int x) {
	int i, j;
	
	printf("\nВведенная матрица:");
	for(i = 0; i < x; i++) { 
		printf("\n\t"); 
		for(j = 0; j < x; j++) { 
			printf("%d ", p[i][j]); 
		} 
	} 
}

matrix3 *getmem(int m ) {
	return (matrix3) malloc(sizeof(int[n]) * m);
}