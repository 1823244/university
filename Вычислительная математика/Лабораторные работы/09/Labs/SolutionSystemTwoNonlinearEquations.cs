using System;

namespace Labs
{
	//Лабораторная работа 7
	/*
	 * Решение системы двух нелинейных уравнений с двумя неизвестными методом Ньютона
	*/
	public static class SolutionSystemTwoNonlinearEquations
	{
		public delegate double Func (double x, double y);
		/*
		 * Решение системы двух нелинейных уравнений с двумя неизвестными методом Ньютона
		 * 
		 * @Fxy				- Первая функция
		 * @dFdx			- Производная первой функции по Х
		 * @dFdy			- Производная первой функции по У
		 * @Gxy				- Вторая функция
		 * @dGdx			- Производная второй функции по Х
		 * @dGdy			- Производная второй функции по У
		 * @x				- Начальное приближение Х
		 * @y				- Начальное приближение У
		 * @epsilon			- Точность
		 * @maxIterations	- Максимальное количество итераций
		 * 
		 * 
		*/
		public static bool NewtonMethod (
			Func Fxy, Func dFdx, Func dFdy,
			Func Gxy, Func dGdx, Func dGdy,
			ref double x, ref double y,
			double epsilon,
			int maxIterations)
		{

			double Fm0 = 0, dFdxm0 = 0, dFdym0 = 0;
			double Gm0 = 0, dGdxm0 = 0, dGdym0 = 0;
			double h = 0, l = 0, delta = 0;

			Fm0 = -Fxy (x, y);
			Gm0 = -Gxy (x, y);

			while (Math.Abs (Fm0) + Math.Abs (Gm0) > epsilon && maxIterations > 0) {
				dFdxm0 = dFdx (x, y);
				dFdym0 = dFdy (x, y);

				dGdxm0 = dGdx (x, y);
				dGdym0 = dGdy (x, y);

				delta = dFdxm0 * dGdym0 - dFdym0 * dGdxm0;

				h = (Fm0 * dGdym0 - dFdym0 * Gm0) / delta;
				l = (dFdxm0 * Gm0 - Fm0 * dGdym0) / delta;

				x = x + h;
				y = y + l;

				Fm0 = -Fxy (x, y);
				Gm0 = -Gxy (x, y);

				maxIterations--;
			} 

			return maxIterations > 0 ? true : false;
		}
	}
}

