#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int main(void){
	int **matrix, n, i, j; 
	FILE *input, *output;

	setlocale(LC_ALL, "RUS");

	printf("%s", "Задание: В текстовом файле хранятся целочисленные матрицы порядка не более n\n\t (n — const) следующим образом: сначала целое число n — порядок матрицы,\t а затем ее элементы по строкам. Преобразовать файл так,\n\t чтобы элементы хранились по столбцам.");
	getchar();
	printf("\n%s", "Введите необходимые условия в файл file.txt и положите его в одну папку с программой.");
	getchar();
	input = fopen("file.txt", "r");
	output = fopen("tmp.txt", "w");
	while(!feof(input)) {
		fscanf(input, "%d", &n);
		fprintf(output, "%d\n", n);
		matrix = (int **) malloc(n * sizeof(int *));
		for(i = 0; i < n; i++) {
			matrix[i] = (int *) malloc(n * sizeof(int));
		}

		for(i = 0; i < n; i++) {
			for(j = 0; j < n; j++) {
				fscanf(input, "%d", &matrix[i][j]);
			}
		}

		for(j = 0; j < n; j++) {
			for(i = 0; i < n; i++) {
				fprintf(output, "%d ", matrix[i][j]);
			}
			fprintf(output, "\n");
		}
	}

	fclose(input);
	fclose(output);
	remove("file.txt");
	rename("tmp.txt", "file.txt");

	printf("\n%s", "Программа завершена без ошибок. Проверьте файл file.txt.");

	getchar();

	return 0;
}