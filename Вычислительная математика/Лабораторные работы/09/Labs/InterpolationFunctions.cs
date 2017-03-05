using System;

namespace Labs
{
	//Лабораторная работа 1 
	public static class InterpolationFunctions
	{
		//Делеагат функции одной переменной
		public delegate double FuncX (double x);
		/*
		 * Интерполяция - способ нахождения промежуточных значений некоторой величины
		 * 	по имеющемуся набору отдельно известных данных.
		*/

		#region Метод Лагранжа

		/*
		 * Строит интерполяционный многочлен Лагранжа уровней 0..level
		 * Вычисляет точное, приближенное значения, а так же погрешности
		 * абсолютную и относительную.
		 * 
		 * @func		- Наша функция
		 * @level		- Максимальная степень многочлена
		 * @countNode	- Число узлов
		 * @a			- точка, в которой нужно найти значение
		 * @writeResult	- Выводить ли все результаты в консоль
		 * 
		 * #yT			- игрик точное
		 * #tabl		- таблица значений функции при x_i
		 * 
		 * Алгоритм:
		 * 	1) Заполняем таблицу значениями функции в заданных точках
		 * 	2) Вычисляем многочлен Лагранжа степеней 0..level
		 * 		a) Находим индекс самого близкого значения к а
		 * 		б) Записываем индексы элементов, которые будут учавствовать в вычислениях(наши x_i)
		 * 		в) Вычисляем выражения вида y_i*l_i^(n),
		 * 			где l_i^(n) = (... * (x-x_(i-1) * (x-x_(i+1) * ...)) / (... * (x_i-x_(i-1) * (x_i-x_(i+1) * ...))
		 *
		*/
		public static void LagrangeMethod (FuncX func, int level, int countNode, int a, bool writeResult)
		{
			double yT;
			double[,] tabl = new double[countNode, 2];
			;

			LoadTabl (func, out yT, ref tabl, a, writeResult);

			Solution (a, level, tabl, yT, writeResult);
		}
		//Заполнение таблицы (вводим x - он считает y)
		private static void LoadTabl (FuncX func, out double yT, ref double[,] tabl, double a, bool writeResult)
		{
			Console.Write ("Введите через пробел {0} значений x: ", tabl.GetLength (0));
			string[] tmp = Console.ReadLine ().Split (' ');

			yT = func (a);
			if (writeResult) {
				Console.WriteLine ("Точное значение: {0:f6}", yT);
				Console.WriteLine ("{0}\t{1}\t{2}", "i", "x", "y");
			}
			for (int i = 0; i < tabl.GetLength (0); i++) {
				tabl [i, 0] = Convert.ToDouble (tmp [i]);
				tabl [i, 1] = Math.Pow (Math.Log (tabl [i, 0]), 2);
				if (writeResult) {
					Console.WriteLine ("{0}\t{1:f0}\t{2:f6}", i, tabl [i, 0], tabl [i, 1]);
				}
			}
			if (writeResult) {
				Console.WriteLine ("Таблица заполнена!\n");
			}
		}
		//Возвращает индекс самого близкого значения к а в таблице
		private static int indextMaxMinA (double[,]tabl, double a)
		{
			for (int i = 0; i < tabl.GetLength (0); i++) {
				if (tabl [i, 0] > a) {
					return i - 1; 
				}
			}
			return -1;
		}
		//Вычисление y_i*l_i^(n)
		private static double OneLevel (int level, int[] b, double a, double[,]tabl)
		{
			double result = 1;  //Числитель
			double znamen = 1;  //Знаменатель

			for (int i = 0; i < b.Length; i++) {
				if (i != level) {
					result *= (a - tabl [b [i], 0]);   //(x - xi)
					znamen *= (tabl [b [level], 0] - tabl [b [i], 0]);   //(xj - xi)
				}
			}

			return tabl [b [level], 1] * result / znamen;
		}
		//Вычисление
		private static void Solution (double a, double level, double[,]tabl, double yT, bool writeResult)
		{
			for (int i = 0; i < level; i++) {
				string strResult = "L" + (i + 1) + "(a)[";
				double result = 0;
				int index = indextMaxMinA (tabl, a);
				index = (index - i >= 0) ? index - i : 0;

				int[] b = new int[i + 2];
				for (int j = 0; j < b.Length; j++) {
					b [j] = index + j;
					strResult += ("x" + j + "=" + tabl [b [j], 0] + " ");
				}
				strResult += "] = ";

				double oneLevelResult = 0;
				for (int j = 0; j < b.Length; j++) {
					oneLevelResult = OneLevel (j, b, a, tabl);
					result += oneLevelResult;
					strResult += String.Format ("{0}{1:f6} ", (j == 0 ? "" : "+ "), oneLevelResult);
				}

				Console.WriteLine (strResult + "= " + result);
				if (writeResult) {
					double delta = Math.Abs (yT - result);
					Console.WriteLine ("delta = {0:f6}", delta);
					Console.WriteLine ("b = {0:f6}", Math.Abs (delta / yT * 100));
				}
			}
		}

		#endregion

		#region Метод Ньютона

		/*
		 * Составляет интерполяционный многочлен Ньютона
		 * Для неравномерного шага.
		 * Вычисляет точное, приближенное значения, а так же погрешности
		 * абсолютную и относительную.
		 * 
		 * @func		- Наша функция
		 * @countNode	- Число узлов
		 * @a			- точка, в которой нужно найти значение
		 * @interpolateForward	- Интерополировать вперед?
		 * @writeResult	- Выводить ли все результаты в консоль
		 * 
		 * #yT			- игрик точное
		 * #tabl		- таблица значений функции при x_i
		 * #matr		- Таблица разделенных разностей
		 * 
		 * Алгоритм:
		 * 	1) Заполняем таблицу значениями функции в заданных точках
		 * 	2) Заполняем матрицу разделенных разностей
		 * 	3) Интерполируем вперед или назад
		 * 		а) 
		 *
		*/
		public static void NewtonMethod (FuncX func, int countNode, int a, bool interpolateForward, bool writeResult)
		{
			double yT;
			double[,] tabl = new double[countNode, 2];
			double[,] matr = new double[countNode, countNode];

			LoadTabl (func, out yT, ref tabl, a, writeResult);

			LoadRazdRaznost (tabl, ref matr);

			if (interpolateForward) {
				SolutionNutonVpered (tabl, matr, a, yT, writeResult);
			} else {
				SolutionNutonNazad (tabl, matr, a, yT, writeResult);
			}
		}
		//Заполняем матрицу разделенных разностей
		private static void LoadRazdRaznost (double[,]tabl, ref double[,]matr)
		{
			int n = matr.GetLength (0);
			for (int i = 0; i < matr.GetLength (1); i++) {
				for (int j = 0; j < n; j++) {
					if (i == 0) {
						matr [j, i] = tabl [j, 1];
					} else {
						matr [j, i] = (matr [j, i - 1] - matr [j + 1, i - 1]) / (tabl [j, 0] - tabl [j + i, 0]);
					}
				}
				n--;
			}

			Console.WriteLine ("______________________");
			for (int i = 0; i < matr.GetLength (0); i++) {
				for (int j = 0; j < matr.GetLength (1); j++) {
					Console.Write ("{0:f6}  ", matr [i, j]);
				}
				Console.WriteLine ();
			}
			Console.WriteLine ("______________________");
		}
		//Интерполирование вперед
		private static void SolutionNutonVpered (double[,] tabl, double[,]matr, double a, double yT, bool writeResult)
		{
			double result = 0;
			int n = matr.GetLength (0);

			for (int i = 0; i < n; i++) {
				double x = 1;
				for (int j = 0; j < i; j++) {
					x *= (a - tabl [j, 0]);
				}

				result += matr [0, i] * x;
			}

			Console.WriteLine ("Nv = {0}", result);
			if (writeResult) {
				double delta = Math.Abs (yT - result);
				Console.WriteLine ("delta = {0:f10}", delta);
				Console.WriteLine ("b = {0:f10}", Math.Abs (delta / yT * 100));
			}
		}
		//Интерполирование назад
		private static void SolutionNutonNazad (double[,] tabl, double[,]matr, double a, double yT, bool writeResult)
		{
			double result = 0;
			int n = matr.GetLength (0);

			for (int i = n - 1; i >= 0; i--) {
				double x = 1;
				for (int j = n - 1; j > i; j--) {
					x *= (a - tabl [j, 0]);
				}

				result += matr [i, n - 1] * x;
			}

			Console.WriteLine ("Nn = {0}", result);
			if (writeResult) {
				double delta = Math.Abs (yT - result);
				Console.WriteLine ("delta = {0:f10}", delta);
				Console.WriteLine ("b = {0:f10}", Math.Abs (delta / yT * 100));
			}
		}

		#endregion
	}
}

