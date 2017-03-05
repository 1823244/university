/*
	Functions of search
*/

#include <stdlib.h>
#include <math.h>
#include <string.h>
#include <iostream>

int linearSearch(int arr[], int left, int right, int element, int *counter) {
	
	int i = left;

	while ((i < right) && arr[i++] != element) (*counter) += 2;
	i--;
	(*counter)++;

	return (arr[i] == element) ? i : -1;
}

int quickLinearSearch(int arr[], int left, int right, int element, int *counter) {

	int i = left;

	arr[right++] = element;
	while (arr[i++] != element) (*counter)++;
	(*counter)++;

	return (i == right) ? -1 : i - 1;
}

int quickLinearSearchSorted(int arr[], int left, int right, int element, int *counter) {

	int i = left;

	if (arr[left] > element) 
		return -1;

	arr[right++] = element;
	while (arr[i++] < element) (*counter)++;
	(*counter)++;

	return (i == right) ? -1 : i - 1;
}

int binarySearch(int arr[], int left, int right, int element, int *counter) {
	
	int mid = 0;

	while (left < right) {
		(*counter)++;
		mid = left + (right - left) / 2;

		(*counter)++;
		if (arr[left] == element)
			return left;

		(*counter)++;
		if (arr[mid] == element) {
			(*counter)++;
			if (mid == left + 1)
				return mid;
			else
				right = mid + 1;
		}
		else if (arr[mid] > element) {
			(*counter)++;
			right = mid;
		}
		else {
			(*counter)++;
			left = mid + 1;
		}
	}

	return -1;
}

int chooseSearch(int arr[], int left, int right, int element, int number, int *counter) {
	(*counter)++;

	if (number > 15)
		return binarySearch(arr, left, right, element, counter);
	else
		return linearSearch(arr, left, right, element, counter);
}

int mBlockSearch(int arr[], int left, int right, int element, int *counter) {

	int N = right - left;
	int sqrtN = (int)floor(sqrt((double)N));
	int	i = sqrtN;
	int j = sqrtN - 1;

	(*counter)++;
	if (arr[left] > element)
		return -1;

	while (j-- && (arr[i] < element)) {
		(*counter)++;
		i += sqrtN;
	}
	
	(*counter)++;
	if (j) 
		return chooseSearch(arr, i - sqrtN + 1, i + 1, element, sqrtN, counter);
	else {
		left = i + 1;
		return chooseSearch(arr, left, right, element, right - left, counter);
	}
}