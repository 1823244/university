using System;

namespace Labs
{
	public static class MatrixOperations
	{
		public static double[,] Addition (double[,]a, double[,]b)
		{
			if (a.GetLength (0) != b.GetLength (0) || a.GetLength (1) != b.GetLength (1)) {
				return null;
			}
			double[,] c = new double[a.GetLength (0), a.GetLength (1)];

			for (int i = 0; i < a.GetLength (0); i++) {
				for (int j = 0; j < a.GetLength (1); j++) {
					c [i, j] = a [i, j] + b [i, j];
				}
			}

			return c;
		}

		public static double[,] Transp (double[,]a)
		{
			double[,] c = new double[a.GetLength (0), a.GetLength (1)];

			for (int i = 0; i < a.GetLength (0); i++) {
				for (int j = 0; j < a.GetLength (1); j++) {
					c [i, j] = a [j, i];
				}
			}

			return c;
		}

		public static double[,] Multiplication (double[,]a, double[,]b)
		{
			if (a.GetLength (1) != b.GetLength (0)) {
				return null;
			}
			double[,] c = new double[a.GetLength (0), b.GetLength (1)];

			for (int i = 0; i < a.GetLength (0); i++) {
				for (int j = 0; j < a.GetLength (1); j++) {
					for (int k = 0; k < a.GetLength (1); k++) {
						c [i, j] += a [i, k] * b [k, j];
					}
				}
			}

			return c;
		}

		public static double[] Multiplication (double[,]a, double[]b)
		{
			if (a.GetLength (1) != b.Length) {
				return null;
			}
			double[] c = new double[b.Length];

			for (int i = 0; i < a.GetLength (0); i++) {
				for (int j = 0; j < a.GetLength (1); j++) {
					c [i] += a [i, j] * b [j];
				}
			}

			return c;
		}

		public static double[] Multiplication (double[]a, double[,]b)
		{
			if (a.Length != b.GetLength (0)) {
				return null;
			}
			double[] c = new double[a.Length];

			for (int i = 0; i < b.GetLength (1); i++) {
				for (int j = 0; j < b.GetLength (0); j++) {
					c [i] += a [j] * b [j, i];
				}
			}

			return c;
		}

		public static double[,] Multiplication (double[,]a, double b)
		{
			double[,] c = new double[a.GetLength (0), a.GetLength (1)];

			for (int i = 0; i < a.GetLength (0); i++) {
				for (int j = 0; j < a.GetLength (1); j++) {
					c [i, j] = a [i, j] * b;
				}
			}

			return c;
		}
	}
}

