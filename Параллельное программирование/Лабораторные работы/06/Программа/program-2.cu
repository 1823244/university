#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>
#include <climits>
#include <cuda_runtime.h>

#define MAX_BLOCKS 65535

/* максимальное количество потоков в блоке;
 * получено так:
 * cudaGetDeviceProperties(&deviceProp, i);
 * printf("Max threads per block: %d\n", deviceProp.maxThreadsPerBlock); 
 */
#define MAX_THREADS 512

// количество итераций в каждом потоке
#define PARTS 1000

__global__ void kernel(long long countStep, int *vector, int *result)
{
	long long step = blockIdx.y * MAX_BLOCKS * MAX_THREADS + (blockIdx.x * blockDim.x + threadIdx.x);
    
	if (step > countStep) {
		return;
    }
	
	// находим минимальное
	int min = INT_MAX;
	for (long long i = step * PARTS; i < step + PARTS; i++)
	{
		if (min > vector[i]) {
			min = vector[i];
		}
	}

	result[step] = min;
}

__host__ int calculateOnHost(long long countStep, int *vector) 
{
	// находим минимальное
	int min = INT_MAX;
	for (long long i = 0; i < countStep; i++)
	{
		if (min > vector[i]) {
			min = vector[i];
		}
	}

	return min;
}

int main(int argc, char** argv)
{
    int multiplier = 6;
    printf("Array size: 10^%d\n", multiplier);

    long long countStep = (long long)pow(10.0, multiplier);

	// объявлем массив и заполняем его данными
	int *vector = new int[countStep];
	srand(time(NULL));
	for (long long i = 0; i < countStep; i++)
	{
		vector[i] = rand();
	}

	// подсчитываем количество блоков
    long long countBlocks = countStep / PARTS;

	// если после деления остался остаток -- добавляем ещё блок
    if (countStep % PARTS != 0) {
        countBlocks++;
    }

	// высчитываем количество блоков и строк в сетке
	int countY = 1;
	if (countBlocks > MAX_BLOCKS) {
		countY = 1 + countBlocks / MAX_BLOCKS;
		countBlocks = MAX_BLOCKS;
	}
	
	// def: dim3(unsigned int vx = 1, unsigned int vy = 1, unsigned int vz = 1
    dim3 gridDim = dim3(countBlocks, countY);
    dim3 blockDim = dim3(PARTS);
    
    int *result = new int[countStep];
    long long size = countStep * sizeof(int);

	printf("countStep = %d\n", countStep);

	/* CUDA START */
	// del
	time_t startTimeHost = clock();
	// end del

	// инициализируем события, чтобы потом посчитать время
	cudaEvent_t start, stop;
	cudaEventCreate(&start);
	cudaEventCreate(&stop);
	
	// запускаем событие начала
	cudaEventRecord(start, 0);
	
	printf("Start CUDA\n");

	int count = countStep;
	while (count) {
		int *resultDev;
		// выделяем память
		long long size = count * sizeof(int);
		cudaMalloc((void**) &resultDev, size);
		// запускаем вычисления
		kernel<<<gridDim, blockDim>>>(count, vector, resultDev);
		// барьерная синхронизация
		cudaDeviceSynchronize();
		// копируем данные в vector из resultDev размера size из GPU
		cudaMemcpy(vector, resultDev, size, cudaMemcpyDeviceToHost);
		// чистим память
		cudaFree(resultDev);
		count /= PARTS;
	}
	
	// запускаем событие завершения работы
	cudaEventRecord(stop, 0);
	// ждём, пока всё завершится
	cudaEventSynchronize(stop);
	printf("Stop CUDA\n");

	float totalTimeDevice;
	// вычисляем время
	cudaEventElapsedTime(&totalTimeDevice, start, stop);

	// del
	time_t endTimeHost = clock();
	double totalTimeHost = (double)(endTimeHost - startTimeHost) / CLOCKS_PER_SEC * 1000;
	// end del
	printf("\nTime on device (ms): %1.3lf\n", totalTimeHost);
	
	// удаляем события
	cudaEventDestroy(start);
	cudaEventDestroy(stop);

	// чистим память
	free(vector);
	vector = NULL;

	/* CUDA END */

	/* HOST START */
	vector = new int[countStep];
	for (long long i = 0; i < countStep; i++)
	{
		vector[i] = rand();
	}

	// засекаем время
	startTimeHost = clock();

	// подсчитываем
	calculateOnHost(countStep, vector);

	// считаем разницу во времени
	endTimeHost = clock();
	totalTimeHost = (double)(endTimeHost - startTimeHost) / CLOCKS_PER_SEC * 1000;

	printf("\nTime on host (ms): %1.3lf\n", totalTimeHost);

	/* HOST END */

	getchar();

	// чистим память
	free(vector);
	cudaDeviceReset();

	return 0;
}
