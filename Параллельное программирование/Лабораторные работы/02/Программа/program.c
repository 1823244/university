#include <iostream>
#include <iomanip>
#include <stdlib.h>
#include <math.h>
#include <omp.h>

using namespace std;
 
long double f(long double x) {
	return sqrt(fabs(cos(tan(sin(x * x * x + x * x + 2 * x))))) * pow(x, 2);	
}
 
int main(int argc, char const *argv[]) {
	long double a, b, eps, eps10, threads;

	if (argc == 5) {
        a = strtold(argv[1], NULL);
        b = strtold(argv[2], NULL);
        eps = strtold(argv[3], NULL);
        threads = strtold(argv[4], NULL);
    } else {
    	cout << "a: ";
    	cin >> a;
    	cout << "b: ";
    	cin >> b;
    	cout << "eps: ";
    	cin >> eps;
    	cout << "threads: ";
    	cin >> threads;
    }

    eps10 = pow(10, -eps);

	long double res = eps10 + 1, res_new = 0, fA = f(a), fB = f(b);
	long int total = 0;

	double timer = omp_get_wtime();

	for (int i = 2; (i <= 4) || (fabs(res_new - res) > eps10); i *= 2) {

		long double h = (b - a) / (2 * i), sum2 = 0, sum4 = 0, sum = 0, tmp4 = 0, tmp2 = 0;

		omp_set_num_threads(threads);

		#pragma omp parallel for reduction(+:tmp4, tmp2) shared(a, h, i)
		for (int j = 1; j <= 2 * i - 1; j += 2)
		{
			tmp4 += f(a + h * j);
			tmp2 += f(a + h * (j + 1));
		}

		total += 2 * i - 1;

		sum4 += tmp4;
		sum2 += tmp2;
		
		sum = fA + 4 * sum4 + 2 * sum2 - fB;//Отнимаем значение f(b) так как ранее прибавили его дважды. 
		res = res_new;
		res_new = (h / 3) * sum;
	}

	timer = omp_get_wtime() - timer;

	// cout << std::setprecision(eps) << res_new << endl;
	cout << total << ' ' << timer << endl;

	return 0;
}