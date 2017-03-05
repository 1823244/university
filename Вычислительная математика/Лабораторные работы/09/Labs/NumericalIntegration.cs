using System;

namespace Labs
{
	//Лабораторная работа 3
	public static class NumericalIntegration
	{
		//Делеагат функции одной переменной
		public delegate double FuncX (double x);

		#region Метод центральных прямоугольников

		/*
		 * Метод центральных прямоугольников
		 * 
		 * @a		- Начало интервала
		 * @b		- Конец интервала
		 * @n		- Количество шагов
		 * @func	- Наша функция
		 * @eps		- Точность
		 * 
		 * Алгоритм:
		 * 	1) Строим таблицу для x_i и x_(i+1/2)
		 * 	2) Вычисляем I = h * summ_(i=0)^n (y_(i+1/2))
		 * 	3) Проверяем достигли ли мы точности по Рунге-Кутта
		 * 		а) вычисляем две таблицы с с шагом h  и шагом 2h
		 * 		б) вычисляем I для каждой
		 * 		в) Выходим, если 1/3 * |I_2 - I_1| <= epsilon, иначе увеличиваем количество шагов
		 * 
		*/
		public static double rectangleMethodWithRunge (double a, double b, int n, FuncX func, double eps)
		{
			double[,] table1 = CreateTable (a, b, n, func); //Создаем таблицу с n шагов
			double step1 = (b - a) / n;
			n *= 2;
			double[,] table2 = CreateTable (a, b, n, func); //Создаем таблицу в 2n шагов
			double step2 = (b - a) / n;

			PrintTable (table1);
			PrintTable (table2);

			double o1 = 1 / 3;
			double result = rectangleMethod (table2, step2);

			//Вычисляем
			while (eps <= o1 * Math.Abs (result - rectangleMethod (table1, step1))) {
				table1 = table2;
				step1 = step2;
				n *= 2;
				table2 = CreateTable (a, b, n, func);
				step2 = (b - a) / n;
				result = rectangleMethod (table2, step2);
			}
			return result;
		}

		private static double rectangleMethod (double[,]sourceTable, double h)
		{
			double result = 0.0;

			for (int i = 0; i < sourceTable.GetLength (1) - 1; i++) {
				result += sourceTable [3, i];
			}

			return result * h;
		}

		public static double rectangleMethodWithRunge (double a, double b, int n, FuncX func)
		{
			double[,] table1 = CreateTable (a, b, n, func); //Создаем таблицу с n шагов
			double step1 = (b - a) / n;

			PrintTable (table1);

			double result = rectangleMethod (table1, step1);
			return result;
		}

		#endregion

		#region Метод Трапеций
		/*
		 * Метод трапеций
		 * 
		 * @a		- Начало интервала
		 * @b		- Конец интервала
		 * @n		- Количество шагов
		 * @func	- Наша функция
		 * @eps		- Точность
		 * 
		 * Алгоритм:
		 * 	1) Строим таблицу для x_i и x_(i+1/2)
		 * 	2) Вычисляем I = h * ((y_0 + y_n)/ 2 * summ_(i=1)^(n-1) (y_i)
		 * 	3) Проверяем достигли ли мы точности по Рунге-Кутта
		 * 		а) вычисляем две таблицы с с шагом h  и шагом 2h
		 * 		б) вычисляем I для каждой
		 * 		в) Выходим, если 1/3 * |I_2 - I_1| <= epsilon, иначе увеличиваем количество шагов
		 * 
		*/
		public static double trapezoidalRuleWithRunge (double a, double b, int n, FuncX func, double eps)
		{
			double[,] table1 = CreateTable (a, b, n, func); //Создаем таблицу с n шагов
			double step1 = (b - a) / n;
			n *= 2;
			double[,] table2 = CreateTable (a, b, n, func); //Создаем таблицу в 2n шагов
			double step2 = (b - a) / n;

			PrintTable (table1);
			PrintTable (table2);

			double o1 = 1 / 3;
			double result = trapezoidalRule (table2, step2);

			//Вычисляем
			while (eps <= o1 * Math.Abs (result - trapezoidalRule (table1, step1))) {
				table1 = table2;
				step1 = step2;
				n *= 2;
				table2 = CreateTable (a, b, n, func);
				step2 = (b - a) / n;
				result = trapezoidalRule (table2, step2);
			}
			return result;
		}

		private static double trapezoidalRule (double[,] sourceTable, double h)
		{
			double result = (sourceTable [1, 0] + sourceTable [1, sourceTable.GetLength (1) - 1]) / 2;

			for (int i = 1; i < sourceTable.GetLength (1) - 1; i++) {
				result += sourceTable [1, i];
			}

			return result * h;
		}
		#endregion

		#region Метод Симпсона (парабол)
		/*
		 * Метод Симпсона (парабол)
		 * 
		 * @a		- Начало интервала
		 * @b		- Конец интервала
		 * @n		- Количество шагов
		 * @func	- Наша функция
		 * @eps		- Точность
		 * 
		 * Алгоритм:
		 * 	1) Строим таблицу для x_i и x_(i+1/2)
		 * 	2) Вычисляем I = h / 6 * summ_(i=0)^(n-1) (y_i + 4*y_(i+1/2) + y_(i+1)) 
		 * 	3) Проверяем достигли ли мы точности по Рунге-Кутта
		 * 		а) вычисляем две таблицы с с шагом h  и шагом 2h
		 * 		б) вычисляем I для каждой
		 * 		в) Выходим, если 1/3 * |I_2 - I_1| <= epsilon, иначе увеличиваем количество шагов
		 * 
		*/
		public static double simpsonRuleWithRunge(double a, double b, int n, FuncX func, double eps)
		{
			double[,] table1 = CreateTable(a, b, n, func); //Создаем таблицу с n шагов
			double step1 = (b - a) / n;
			n *= 2;
			double[,] table2 = CreateTable(a, b, n, func); //Создаем таблицу в 2n шагов
			double step2 = (b - a) / n;

			PrintTable(table1);
			PrintTable(table2);

			double o1 = 1 / 15;
			double result = simpsonRule(table2, step2);

			//Вычисляем
			while (eps <= o1 * Math.Abs(result - simpsonRule(table1, step1)))
			{
				table1 = table2; step1 = step2;
				n *= 2;
				table2 = CreateTable(a, b, n, func); step2 = (b - a) / n;
				result = simpsonRule(table2, step2);
			}
			return result;
		}
		private static double simpsonRule(double[,] sourceTable, double h)
		{
			double result = 0.0;

			for (int i = 0; i < sourceTable.GetLength(1)-1; i++)
			{
				result += sourceTable[1, i] + 4 *  sourceTable[3, i] + sourceTable[1, i+1];
			}

			return result * h / 6;
		}
		#endregion

		#region Метод Гаусса
		/*
		 * Метод Гаусса
		 * 
		 * @a		- Начало интервала
		 * @b		- Конец интервала
		 * @n		- Количество шагов
		 * @func	- Наша функция
		 * 
		 * Алгоритм:
		 * 	1) Строим таблицу для x_i и x_(i+1/2)
		 * 	2) Вычисляем I = h * summ_(i=0)^n (y_(i+1/2)
		 * 	3) Проверяем достигли ли мы точности по Рунге-Кутта
		 * 		а) вычисляем две таблицы с с шагом h  и шагом 2h
		 * 		б) вычисляем I для каждой
		 * 		в) Выходим, если 1/3 * |I_2 - I_1| <= epsilon, иначе увеличиваем количество шагов
		 * 
		*/
		public static double Gauss(double a, double b, int n, FuncX func)
		{
			double[,] table = {
				{0, 2, 0, 0, 0, 0, 0, 0},
				{-0.5773502692, 1, 0.5773502692, 1, 0, 0, 0, 0},
				{-0.7745966692, 0.5555555556, 0, 0.8888888888, 0.7745966692, 0.5555555556, 0, 0},
				{-0.8611363115, 0.3478548451, -0.3399810436, 0.6521451549, 0.8611363115, 0.3478548451, 0.3399810436, 0.6521451549}
			};

			double result = 0.0;
			double tmp = (b - a) / 2;

			for (int i = 0; i < n; i++)
			{
				Console.WriteLine(table[n - 1, i * 2]);
				Console.WriteLine("{0} = {1} * f({2}) = {3}", i,
					table[n - 1, i * 2 + 1],
					(b + a) / 2 + tmp * table[n - 1, i * 2],
					table[n - 1, i * 2 + 1] * func((b + a) / 2 + tmp * table[n - 1, i * 2]));
				result += table[n - 1, i * 2 + 1] * func((b + a) / 2 + tmp * table[n - 1, i * 2]);
			}


			return result * tmp;
		}
		#endregion

		#region Вспомогательные функции

		//Создание таблицы
		private static double[,] CreateTable (double a, double b, int n, FuncX func)
		{
			double[,] table = new double[4, n + 1];
			double step = (b - a) / n;  //Шаг

			//Заполняем с конца столбцы
			//Чтобы сразу заполнять X(i+1/2) и Y
			table [0, n] = b;
			table [1, n] = func (b);
			for (int i = n - 1; i >= 0; i--) {
				table [0, i] = table [0, i + 1] - step;    //Xi
				table [1, i] = func (table [0, i]);    //Yi
				table [2, i] = table [0, i] + (table [0, i + 1] - table [0, i]) / 2;     //X(i+1/2)
				table [3, i] = func (table [2, i]);     //Y(i+1/2)
			}

			return table;
		}
		//Вывод таблицы
		private static void PrintTable (double[,] table)
		{
			for (int i = 0; i < table.GetLength (1); i++) {
				for (int j = 0; j < table.GetLength (0); j++) {
					Console.Write ("{0:f3}\t", table [j, i]);
				}
				Console.WriteLine ();
			}
			Console.WriteLine ("____________________");
		}

		#endregion
	}
}

