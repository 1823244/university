#include "mpi.h"
#include <stdio.h>

int main(int argc, char *argv[])
{
    int proc_num, 
        proc_rank,
        recv_rank; 

    double time, time_min = 1.0/0.0, time_max = 0, time_avg = 0;
    MPI_Status status;
    
    // инициализируем MPI
    MPI_Init(&argc, &argv);
    // proc_num -- общее число процессоров
    MPI_Comm_size(MPI_COMM_WORLD, &proc_num);
    // proc_rank -- ранг текущего процессора
    MPI_Comm_rank(MPI_COMM_WORLD, &proc_rank);

    /*  если процессов меньше 3,
        то нельзя вычленить 2 дочерних из них */
    if (proc_num < 3) {
        if (proc_rank == 0) {
            printf("Need more than 2 child processes!\n");
        }
        return 0;
    }

    // массив для хранения результатов
    auto array = new double[proc_num][2](); 

    // proc_rank == 0 -- главный процесс
    if (proc_rank == 0) {

        /*  принимает информацию от дочерних процессов
            и записывает время её приёма в массив */
        for (int i = 1; i < proc_num * 2 - 1; i++)
        {
            // MPI_Recv(buffer, count, type, from, tag, communicator, status)
            MPI_Recv(&recv_rank, 1, MPI_INT, MPI_ANY_SOURCE,
                     MPI_ANY_TAG, MPI_COMM_WORLD, &status);
            if (array[recv_rank][0] == 0.0) {
                array[recv_rank][0] = MPI_Wtime();
            } else {
                array[recv_rank][1] = MPI_Wtime();
            }
        }

        /*  для всех записанных результатов
            вычисляется время рассогласованности,
            минимальное, максимальное и среднее время */
        for (int i = 1; i < proc_num; i++)
        {
            time = array[i][1] - array[i][0];
            time_avg += time;

            if (time < time_min) {
                time_min = time;
            }

            if (time > time_max) {
                time_max = time;
            }

            printf("Time of process %d: %lf\n",
                    i, time);
        }

        time_avg /= proc_num - 1;
        printf("Min: %lf; Max: %lf; Avg: %lf\n",
                time_min, time_max, time_avg);

    } else {
        // определение соседних дочерних процессов 
        int prev = (proc_rank == 1) ? proc_num - 1 : proc_rank - 1,
            next = (proc_rank + 1 == proc_num) ? 1 : proc_rank + 1,
            buf;

        // отправка информации соседним дочерним процессам
        // MPI_Send(buffer, count, type, to, tag, communicator)
        MPI_Send(&proc_rank, 1, MPI_INT, prev, 0, MPI_COMM_WORLD);
        MPI_Send(&proc_rank, 1, MPI_INT, next, 0, MPI_COMM_WORLD);
        
        // получаем информацию от соседа 'слева' и отсылаем главному процессу
        MPI_Recv(&buf, 1, MPI_INT, prev, 0, MPI_COMM_WORLD, &status);
        MPI_Send(&buf, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
        
        // получаем информацию от соседа 'справа' и отсылаем главному процессу
        MPI_Recv(&buf, 1, MPI_INT, next, 0, MPI_COMM_WORLD, &status);
        MPI_Send(&buf, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
    }

    // завершаем работу MPI
    MPI_Finalize();

    return 0;
}