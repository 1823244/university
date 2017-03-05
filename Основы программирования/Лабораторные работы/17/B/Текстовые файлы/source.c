z#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int getWord(char *string, FILE *input) {
	char c;
	int i = 0;

	while((c = getc(input)) <= ' ' && !feof(input));
	while(c > ' ' && !feof(input)) {
		*string++ = c;
		c = getc(input);
		i++;
	}
	*string = '\0';

	return i;
}

int main(void) {
	FILE *input, *output;
	char *string = (char *) malloc(255 * sizeof(char));
	int n;

	setlocale(LC_ALL, "RUS");

	printf("%s", "Задание: Дан символьный файл. Сократить число пробелов между словами до одного");
	getchar();
	printf("\n%s", "Введите необходимые условия в файл file.txt и положите его в одну папку с программой.");
	getchar();

	input = fopen("file.txt", "r");
	output = fopen("tmp.txt", "w");
	n = getWord(string, input);
	fprintf(output, "%s", string);

	while(!feof(input)) {
		n = getWord(string, input);
		if (n != 0) {
			fprintf(output, " %s", string);
		}
	}

	fclose(input);
	fclose(output);
	remove("file.txt");
	rename("tmp.txt", "file.txt");
	return 0;
}