using System;

namespace Labs
{
	//Лабораторная работа 6
	public static class MultidimensionalMinimization
	{
		public delegate double Func (double x, double y);
		/*
		 * Метод градиента с дроблением шага
		 * 
		 * @func		- Наша функция
		 * @gradFuncX	- Функция градиента по X
		 * @gradFuncY	- Функция градиента по Y
		 * @x			- Начальный x
		 * @y			- Начальный y
		 * @epsilon		- Точность
		 * @maxIterations - Максимальное количество итераций
		 * @alpha		- Коэффициент >0
		 * @beta		- Коэффициент >0
		 * @gamma		- Коэффициент 0 < gamma < 1
		 * 
		 * @Return	true - 	если приближенное решение с заданной
		 * 					точностью получено за число итераций
		 * 			false -	в противном случае
		 * 
		*/
		public static bool GradientMethodWithFractionalSteps (
			Func func,
			Func gradFuncX,
			Func gradFuncY,
			double x, double y,
			double epsilon,
			int maxIterations,
			double alpha, double beta, double gamma)
		{
			double x1 = x;
			double y1 = y;
			double powerGamma = 0; 	//gamma ^ i
			double leftPart, rightPart;
			double gradX, gradY;
			int iteration = 0;

			while (iteration < maxIterations) {
				powerGamma = 1;

				//Вычисляем градиент! для начальной точки
				gradX = gradFuncX (x, y);
				gradY = gradFuncY (x, y);

				//Вычисляем координаты следующей точки
				x1 = x - alpha * powerGamma * gradX;
				y1 = y - alpha * powerGamma * gradY;

				//Проверяем условие остановки
				if (Math.Abs (func (x1, y1) - func (x, y)) <= epsilon) {
					return true;
				}
					
				//Вычисляем левую и правую части неравенства
				leftPart = func (x1, y1) - func (x, y);
				rightPart = -alpha * beta * powerGamma *
					(Math.Pow (gradX, 2) + Math.Pow (gradY, 2));
				while (leftPart > rightPart) {
					powerGamma *= gamma;
					x1 = x - alpha * powerGamma * gradX;
					y1 = y - alpha * powerGamma * gradY;

					leftPart = func (x1, y1) - func (x, y);
					rightPart = -alpha * beta * powerGamma *
						(Math.Pow (gradX, 2) + Math.Pow (gradY, 2));
				}
					
				x = x1;
				y = y1;
				iteration++;
			}



			return false;
		}
	}
}

