#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int main(void){
	int **matrix, n, i, j; 
	FILE *input, *output;

	setlocale(LC_ALL, "RUS");

	printf("%s", "�������: � ��������� ����� �������� ������������� ������� ������� �� ����� n\n\t (n � const) ��������� �������: ������� ����� ����� n � ������� �������,\t � ����� �� �������� �� �������. ������������� ���� ���,\n\t ����� �������� ��������� �� ��������.");
	getchar();
	printf("\n%s", "������� ����������� ������� � ���� file.txt � �������� ��� � ���� ����� � ����������.");
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

	printf("\n%s", "��������� ��������� ��� ������. ��������� ���� file.txt.");

	getchar();

	return 0;
}