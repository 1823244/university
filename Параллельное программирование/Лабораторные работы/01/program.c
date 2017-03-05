#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <omp.h>

#ifndef M_PI

#define M_PI 3.14159265358979323846264338327

#endif

int main(int argc, char const *argv[])
{
    long double a, b, h, threads, val = M_PI / 180;

    if (argc == 5) {
        a = strtold(argv[1], NULL);
        b = strtold(argv[2], NULL);
        h = strtold(argv[3], NULL);
        threads = strtold(argv[4], NULL);
    } else {
        printf("a: ");
        scanf("%lf", &a);
        printf("b: ");
        scanf("%lf", &b);
        printf("h: ");
        scanf("%lf", &h);
        printf("threads: ");
        scanf("%d", &threads);
    }

    long int steps = (b - a) / h;
    
    // double result[steps];
    long double result;

    omp_set_num_threads(threads);

    double timer = omp_get_wtime();
    
    #pragma omp parallel for shared(h, val, a, steps)
    for (long int i = 0; i < steps; i++) {
        result = sqrt(abs(cos(tan(sin((a + h * i) * val)))));
    }

    timer = omp_get_wtime() - timer;

    printf("%lf\n", timer);

    return 0;
}