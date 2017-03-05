using System;

namespace Labs
{
	public static class EigenValuesVectors
	{
		public static double Norma2(double[] a) {
			double result = 0;

			for (int i = 0; i < a.Length; i++) {
				result += a[i] * a [i];
			}

			return Math.Sqrt(result);
		}
		public static double[] Multiplication(double n, double[] x) {
			double[] c = new double[x.Length];

			for (int i = 0; i < x.Length; i++) {
				c [i] = x [i] * n;
			}

			return c;
		}
		public static double Multiplication(double[] a, double[] b) {
			if (a.Length != b.Length) {
				return 0;
			}
			double c = 0;

			for (int i = 0; i < a.Length; i++) {
				c += a [i] * b[i];
			}

			return c;
		}

		public static void MaxValue(double[,] A, double[] X, double epsilon) {
			double k = 1 / Norma2(X);
			X = Multiplication (k, X);

			double[] y1 = MatrixOperations.Multiplication (A, X);
			double lambda1 = Multiplication (X, y1);
			double[] x1 = Multiplication (1 / Norma2 (y1), y1);

			double[] y2 = MatrixOperations.Multiplication (A, x1);
			double lambda2 = Multiplication (x1, y2);
			double[] x2 = Multiplication (1 / Norma2 (y2), y2);

			while (lambda2 - lambda1 > epsilon) {
				y1 = y2; lambda1 = lambda2; x1 = x2;

				y2 = MatrixOperations.Multiplication (A, x1);
				lambda2 = Multiplication (x1, y2);
				x2 = Multiplication (1 / Norma2 (y2), y2);
			}

			Console.Write ("Собственное число: {0:F6}\nНорма вектора: {1}\nВектор: ", lambda2, Norma2(x2));
			for (int i = 0; i < x2.Length; i++) {
				Console.Write ("{0} ", x2[i]);
			}
		}
	}
}

