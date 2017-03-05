#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

void createFile() {
	char c;
	FILE *file;
	
	file = fopen("file", "wb");
	
	printf("%s\n\t", "Введите содержимое файла:");
	while((c = getchar()) != '\n') {
		fwrite(&c, sizeof(char), 1, file);
	}

	fclose(file);
	printf("\n");

}

void printFile(FILE *file) {
	char c;
	int flag = 1;

	printf("%s\n\t", "Содержимое файла:");
	while(flag) {
		fread(&c, sizeof(char), 1, file);
		if (!feof(file)) {
			printf("%c", c);
		} else {
			flag = 0;
		}
	}

	printf("\n\n");
}

int main(void) {
	FILE *input, *output;
	char *string = (char *) malloc(1 * sizeof(char)), c;
	int n, space = 1, flag = 1;

	setlocale(LC_ALL, "RUS");

	printf("%s", "Задание: Дан символьный файл. Сократить число пробелов между словами до одного.");
	getchar();
	createFile();

	input = fopen("file", "rb");
	output = fopen("tmp", "w+b");

	while(flag) {
		fread(&c, sizeof(char), 1, input);

		if(!feof(input)) {
			if((space == 0) || (c > ' ')) {
				fwrite(&c, sizeof(char), 1, output);
			}
		} else {
			flag = 0;
		}

		space = (c > ' ') ? 0 : 1;
	}

	rewind(output);
	printFile(output);
	fclose(input);
	fclose(output);
	remove("file");
	rename("tmp", "file");

	printf("\n\n%s", "Нажмите [Enter] для завершения работы программы...");
	getchar();
	
	return 0;
}