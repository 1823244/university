/*
	Main program
*/

#include <iostream>
#include "searches.h"
#include <time.h>

#define N 10000
#define TESTS 1000

void generate(int arr[], int *max, int *mid, bool mode) {

	int counter = 0, sum = 0;
	int i = 0, j = 0;

	*max = 0;

	for (i = 0; i < TESTS; i++) {
		if (mode) {
			for (j = 0; j < N; j++)
				arr[j] = rand() % N;
		}
		else {
			for (j = 0; j < N; j++)
				arr[j] = j;
		}

		mBlockSearch(arr, 0, N, ((rand() % (N + N/10)) - N/10),  &counter);

		if (counter > *max)
			*max = counter;

		sum += counter;
		counter = 0;
	}

	*mid = (int)(sum / TESTS);
}

int main() {

	int arr[N + 1];
	int max, mid;

	srand((unsigned)time(NULL));

	generate(arr, &max, &mid, false);
	printf("Max: %d, Mid: %d\n", max, mid);

	return 0;
}