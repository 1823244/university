using System;

namespace Lab8
{
	public delegate double Func(double x);

	public static class CombinedMethod
	{
		public static double CombinedMethodFunc (
			Func F,
			Func dFdx,
			Func ddFdx,
			double a, double b,
			double epsilon)
		{
			//x10 - x0 хорды
			double x10, x20;
			double x11, x21;

			if (F (a) * ddFdx (a) > 0) {
				x10 = b;
				x20 = a;
			} else {
				x10 = a;
				x20 = b;
			}

			while (true) {
				//Метод касательной
				x21 = x20 - F (x20) / dFdx (x20);
				//Метод хорд
				x11 = x10 - (F (x10) * (x21 - x10)) / (F (x21) - F (x10));

				if (Math.Abs (x10 - x20) < epsilon) {
					return (x11 + x21) / 2;
				} else {
					x10 = x11;
					x20 = x21;
				}
			}
		}
	}
}

