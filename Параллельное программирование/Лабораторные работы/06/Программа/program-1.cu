#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>
#include <cuda_runtime.h>

#define N 1
#define MAX_BLOCKS 65535

/* максимальное количество потоков в блоке;
 * получено так:
 * cudaGetDeviceProperties(&deviceProp, i);
 * printf("Max threads per block: %d\n", deviceProp.maxThreadsPerBlock); 
 */
#define MAX_THREADS 512

__device__ __host__ float f(float x)
{
	return sqrtf(x - 1) + 1 / (x - 3);
}

__global__ void kernel(long long countStep, float h, float *result)
{
	long long step = blockIdx.y * MAX_BLOCKS * MAX_THREADS + (blockIdx.x * blockDim.x + threadIdx.x);
    
	if (step > countStep) {
		return;
    }

	result[step] = f(1 + h * step);
}

__host__ void calculateOnHost(long long countStep, float h, float *result) 
{
	for (long long i = 0; i < countStep; i++)
	{
		result[i] = f(1 + h * i);
	}
}

int main(int argc, char** argv)
{
    int multiplier = 4;
    printf("Step: 1 * 10^%d\n", multiplier);

    float k = N * powf(10.0, multiplier);
    float h = N / k;
    printf("k = %f, h = %f\n", k, h);

	// вычисляем количество шагов
    long long countStep = N / h + 1;

	// подсчитываем количество блоков
    long long countBlocks = countStep / MAX_THREADS;

	// если после деления остался остаток -- добавляем ещё блок
    if (countStep % MAX_THREADS != 0) {
        countBlocks++;
    }

	int countY = 1;
	if (countBlocks > MAX_BLOCKS) {
		countY = 1 + countBlocks / MAX_BLOCKS;
		countBlocks = MAX_BLOCKS;
	}
  
	// высчитываем количество блоков и строк в сетке
	// def: dim3(unsigned int vx = 1, unsigned int vy = 1, unsigned int vz = 1
    dim3 gridDim = dim3(countBlocks, countY);
    dim3 blockDim = dim3(MAX_THREADS);
    
    float *result = new float[countStep];
    float *resultDev;
    long long size = countStep * sizeof(float);

	printf("countStep = %d\n", countStep);
	printf("countBlocks = %d\n", countBlocks);

	/* CUDA START */
	// del
	time_t startTimeHost = clock();
	result = new float[countStep]; 
	// end del

	// инициализируем события, чтобы потом посчитать время
	cudaEvent_t start, stop;
	cudaEventCreate(&start);
	cudaEventCreate(&stop);
	
	// запускаем событие начала
	cudaEventRecord(start, 0);
	// выделяем память
	cudaMalloc((void**) &resultDev, size);
	printf("Start CUDA from %d to %d with step = %0.8f\n", 1, N + 1, h);
	// запускаем вычисления
	kernel<<<gridDim, blockDim>>>(countStep, h, resultDev);

	// барьерная синхронизация
	cudaDeviceSynchronize();
	// копируем данные в result из resultDev размера size из GPU
	cudaMemcpy(result, resultDev, size, cudaMemcpyDeviceToHost);
	// чистим память
	cudaFree(resultDev);
	
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
	free(result);
	result = NULL;

	/* CUDA END */

	/* HOST START */

	// засекаем время
	startTimeHost = clock();

	result = new float[countStep]; 

	// подсчитываем
	calculateOnHost(countStep, h, result);

	// считаем разницу во времени
	endTimeHost = clock();
	totalTimeHost = (double)(endTimeHost - startTimeHost) / CLOCKS_PER_SEC * 1000;

	printf("\nTime on host (ms): %1.3lf\n", totalTimeHost);

	/* HOST END */

	getchar();

	// чистим память
	free(result);
	cudaDeviceReset();

	return 0;
}
