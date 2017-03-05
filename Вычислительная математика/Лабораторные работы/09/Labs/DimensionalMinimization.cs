using System;
using System.Collections.Generic;

namespace Labs
{
	//Лабораторная работа 5
	public static class DimensionalMinimization
	{
		//Делегат функции
		public delegate double Func (double x);
		/*
		 * Метод оптимального поиска
		 * 
		 * @a	Левая граница отрезка, на котором находится искомое значение
		 * @b	Правая граница отрезка, на котором находится искомое значение
		 * @func Функция, по которой строился график
		 * @h	Шаг
		 * 
		 * Находим значения на крайних точках отрезка, выбираем минимальное из них.
		 * Проходимся по отрезку с заданным шагом, при этом находим минимальное значение
		 * на отрезке и запоминаем в какой точке было получено это значение.
		 * Это значение и будет является ответом
		*/
		public static double MethodOfOptimalSearch (double a, double b, Func func, double h)
		{
			//При каком x получено минимальное значение
			double xMinValue = a;
			//Найдем минимальное значение на концах отрезка
			double min = func (a);
			double tmp = func (b);
			if (min > tmp) {
				min = tmp;
				xMinValue = b;
			}

			//Ищем минимальное значение среди внутренних точек отрезка
			a += h;
			while (a < b) {
				tmp = func (a);
				if (min > tmp) {
					min = tmp;
					xMinValue = a;
				}
				a += h;
			}
			return xMinValue;
		}
		/*
		 * Метод деления отрезка пополам
		 * 
		 * @a		Левая граница отрезка, на котором находится искомое значение
		 * @b		Правая граница отрезка, на котором находится искомое значение
		 * @func 	Функция, по которой строился график
		 * @epsilon Точность, с которой нужно вычислить значение
		 * 
		 * l=b-a
		 * Вычислять будем до тех пор, пока l>epsilon
		 * Имея a, b находим alpha = l / 2 - ∂ & beta = l / 2 + ∂, где ∂ = epsilon / 2
		 * Затем, вычисляем func(alpha) и func(beta),
		 * если func(alpha) ≤ func(beta), то берем отрезок [a; beta], иначе [alpha; b]
		*/
		public static double MethodOfBisectionOfTheInterval (double a, double b, Func func, double epsilon)
		{
			double l = b - a;

			double delta = epsilon / 2;
			double alpha;
			double beta;

			double f_alpha;
			double f_beta;

			while (Math.Abs (epsilon - l) > 0.00000001) {
				alpha = (a + b) / 2 - delta;
				beta = (a + b) / 2 + delta;

				f_alpha = func (alpha);
				f_beta = func (beta);

				if (f_alpha <= f_beta) {
					b = beta;
				} else {
					a = alpha;
				}

				l = b - a;
			}

			return (a + b) / 2;
		}
		/*
		 * Метод основанный на числах Фибоначчи
		 * 
		 * @a		Левая граница отрезка, на котором находится искомое значение
		 * @b		Правая граница отрезка, на котором находится искомое значение
		 * @func 	Функция, по которой строился график
		 * @epsilon Точность, с которой нужно вычислить значение
		 * 
		 * l=b-a
		 * Вычислять будем до тех пор, пока l>epsilon
		 * Вычисляем, какое по счету число Фибоначчи мы возьмем максимальным
		 * Fn+1 ≥ l / epsilon
		 * Пример:
		 * l = 0.1; epsilon = 0.01
		 * Fn+1 ≥ 10
		 * Шестое число Фибоначчи 13 (1(0) 1(1) 2(2) 3(3) 5(4) 8(5) 13(6))
		 * n+1 = 6; n = 5;
		 * Вычисляем:
		 * 	alpha = a + (Fn-k-1) / (Fn-k+1) * l
		 * 	beta = a + (Fn-k) / (Fn-k+1) * l
		 * 
		 *	f(alpha)
		 *	f(beta)
		 *
		 * Когда точность достигнута result = f(alpha) ≤ f(beta) ? alpha : beta;
		*/
		public static double FibonacciMethod (double a, double b, Func func, double epsilon)
		{
			double l = b - a;
			int n = (int)(l / epsilon);
			List<int> FibonacciArray = GetFibonacciArray (n);

			double alpha;
			double beta;

			double f_alpha;
			double f_beta;

			n = FibonacciArray.Count - 2;
			int k = 0;
			do {
				alpha = a + (FibonacciArray [n - k - 1] / (double)FibonacciArray [n - k + 1]) * l;
				beta = a + (FibonacciArray [n - k] / (double)FibonacciArray [n - k + 1]) * l;

				f_alpha = func (alpha);
				f_beta = func (beta);

				if (f_alpha <= f_beta) {
					b = beta;
				} else {
					a = alpha;
				}

				k++;
				l = b - a;
			} while (l > epsilon);

			return f_alpha <= f_beta ? alpha : beta;
		}
		/* 
		 * Получить массив чисел Фибоначчи(Передается граница в виде числа,
		 * перейдя через которое функция вернет значение)
		 * Например:
		 * 	n = 10
		 *  пятое число = 8, шестое = 13
		 * 	Максимальное число в списке будет 13
		*/
		private static List<int> GetFibonacciArray (int n)
		{
			List<int> result = new List<int> ();

			result.Add (1);
			result.Add (1);
			int i = 2;

			do {
				result.Add (result [i - 2] + result [i - 1]);
			} while(result [i++] < n);

			return result;
		}
		/*
		 * Метод «Золотого сечения»
		 * 
		 * @a		Левая граница отрезка, на котором находится искомое значение
		 * @b		Правая граница отрезка, на котором находится искомое значение
		 * @func 	Функция, по которой строился график
		 * @epsilon Точность, с которой нужно вычислить значение
		 * 
		 * l=b-a
		 * Вычислять будем до тех пор, пока l>epsilon
		 * Находим:
		 * 	alpha = a + (3 - sqrt(5)) / 2 * l
		 * 	beta = a + (sqrt(5) - 1) / 2 * l
		 * 
		 * 	f(alpha)
		 * 	f(beta)
		 * 
		 * Если f(alpha) >= f(beta), то [alpha; b], иначе [a; beta]
		 * 
		 * Когда точность достигнула вычисляем результат (a + b) / 2
		*/
		public static double GoldenSectionMethod (double a, double b, Func func, double epsilon)
		{
			double mnBeta = (Math.Sqrt (5));
			double mnAlpha = (3 - mnBeta
			) / 2;
			mnBeta = (mnBeta - 1) / 2;
			double l = b - a;

			double alpha;
			double beta;

			double f_alpha;
			double f_beta;

			while (l > epsilon) {
				alpha = a + mnAlpha * l;
				beta = a + mnBeta * l;

				f_alpha = func (alpha);
				f_beta = func (beta);

				if (f_alpha >= f_beta) {
					a = alpha;
				} else {
					b = beta;
				}

				l = b - a;
			}

			return (a + b) / 2;
		}
	}
}

