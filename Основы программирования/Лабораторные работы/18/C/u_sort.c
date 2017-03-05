#include <stdio.h>
#include <stdlib.h>
#include "u_sort.h"

void swap(void *a, void *b, size_t size)
{
	char tmp;
	int i = 0;
	for (; i < size; i++) {
		tmp = ((char *)a)[i];
		((char *)a)[i] = ((char *)b)[i];
		((char *)b)[i] = tmp;
	}
}

void sort(void * arr, unsigned length, size_t size, int (*funct)(void *, void *))
{
	char *begin = (char *)arr, *max;
	int i = 0, j;
	for (;i < length - 1; i++) 
	{
		max = begin + i * size;
		for (j = i + 1; j < length; j++)
			if (funct(begin + j * size, max)) {
				max = begin + j * size;
			}
		swap(begin + i * size, max, size);
	}
}