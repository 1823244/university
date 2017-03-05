#include <stdio.h>
#include <stdlib.h>
#include "u_sort.h"

#define marks_number 3

typedef struct {
	char name[50];
	int marks[marks_number];
	float avg;
} student;
	
int lexicographical(void *a, void *b) {
	int i = 0;
	
	for (; (*(student *)a).name[i] && (*(student *)a).name[i] == (*(student *)b).name[i]; i++) ;
	
	return ((*(student *)a).name[i] < (*(student *)b).name[i]);
} 

int nonincreasing(void *a, void *b) {
	return ((*(student *)a).avg > (*(student *)b).avg);
}

void funcStrcat(char *to, char *from) {

	while(*to) to++;
		
	while(*to++ = *from++); 
	
	*(--to) = ' ';
	*(++to) = '\0';

}

size_t readFile(student *arr, unsigned *n) {
	size_t i = 0, j, sum;
	FILE *file;
	char tmp[20];

	if (file = fopen("input.txt", "r")) {
		fscanf(file, "%u\n", &*n);
		if (arr = (student *) malloc (sizeof(student) * *n)) {
			for (; i < *n; i++)  {

				sum = 0;
				arr[i].name[0] = '\0';
				for(j = 0; j < 3; j++) {
					fscanf(file, "%s", tmp); 
					funcStrcat(arr[i].name, tmp);
				}
				for (j = 0; j < marks_number; j++)
				{
					fscanf(file,"%d", &arr[i].marks[j]);
					sum += arr[i].marks[j];
				}
				arr[i].avg = (float)sum/marks_number;

			} 
			return (size_t)arr;
		} else return 1;
	} else return 0;

}

void writeFile(student *arr, unsigned n)
{
	size_t i = 0, j;
	FILE *file;
	
	file = fopen("output.txt", "w");
	
	for (; i < n; i++) 
	{
		fprintf(file, "%s", arr[i].name);
		for (j = 0; j < n; j++)
			fprintf(file, "%d ", arr[i].marks[j]);
		fprintf(file, "%.2f\n", arr[i].avg);
	}	

	fclose(file);
}

int main()
{
	student *arr = (student *) malloc (sizeof(student));
	unsigned n;
	size_t p;
	char type;

	switch (p = readFile(arr, &n))
	{
		case 0: printf("%s", "File not opened."); break;
		case 1: printf("%s", "Not enought memory."); break;
		default:
			arr = (student *)p;
			printf("%s\n", "[L]exicographical or [N]onincreasing?");
			scanf("%c", &type);
			switch (type)
			{
				case 'N':
					sort(arr, n, sizeof(student), &nonincreasing);
				break;
				case 'L':
					sort(arr, n, sizeof(student), &lexicographical);
				break;
			}
			writeFile(arr, n);
		break;
	}	

	getchar();

	return 0;
}