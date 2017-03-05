using System;

namespace Program
{
	class Program
	{
		private const double h_rungekutt = 1.0E-03;
		private const double d = 0.01;

		private delegate double function(double t, double x, double y);
		private static Matrix.Matrix R, K;

		private static double f(double t, double x, double y)
		{
			return x * y - t; // 2.1
		}

		private static double g(double t, double x, double y)
		{
			return Math.Exp(-x); // 2.2
		}

		private static void num_du_rungekutt(function f, function g,
											  double x0, double y0, int iColumn2Write,
											  ref Matrix.Matrix Matr)
		{
			double t = 0, x1 = x0, y1 = y0;
			int j = 0;
			double k1, k2, k3, k4, m1, m2, m3, m4;

			for (int i = 1; i <= 500; i++) // max 5
			{
				t += h_rungekutt;
				k1 = f(t, x1, y1);
				m1 = g(t, x1, y1);
				k2 = f(t + h_rungekutt / 2, x1 + h_rungekutt * k1 / 2, y1 + h_rungekutt * m1 / 2);
				m2 = g(t + h_rungekutt / 2, x1 + h_rungekutt * k1 / 2, y1 + h_rungekutt * m1 / 2);
				k3 = f(t + h_rungekutt / 2, x1 + h_rungekutt * k2 / 2, y1 + h_rungekutt * m2 / 2);
				m3 = g(t + h_rungekutt / 2, x1 + h_rungekutt * k2 / 2, y1 + h_rungekutt * m2 / 2);
				k4 = f(t + h_rungekutt, x1 + h_rungekutt * k3, y1 + h_rungekutt * m3);
				m4 = g(t + h_rungekutt, x1 + h_rungekutt * k3, y1 + h_rungekutt * m3);
				x1 = x1 + h_rungekutt * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
				y1 = y1 + h_rungekutt * (m1 + 2 * m2 + 2 * m3 + m4) / 6;

				if ((i == 100) || (i == 200) || (i == 300) || (i == 400)
					|| (i == 500) || (i == 600) || (i == 700)
					|| (i == 800) || (i == 900) || (i == 1000)) // 5
				{
					Matr[j++, iColumn2Write] = x1 + y1; // 2.3
				}
			}
		}

		private static void Init()
		{
			R = new Matrix.Matrix(10, 1);
			K = new Matrix.Matrix(10, 10);

			// 3
			R[0, 0] = -4.0748217690E+00;
			R[1, 0] = -4.9251244659E+00;
			R[2, 0] = -5.3650596817E+00;
			R[3, 0] = -5.5725335790E+00;
			R[4, 0] = -5.6489380624E+00;
			R[5, 0] = -5.6516105985E+00;
			R[6, 0] = -5.6127699478E+00;
			R[7, 0] = -5.5501570365E+00;
			R[8, 0] = -5.4736066877E+00;
			R[9, 0] = -5.3882442310E+00;

			// 4
			K[0, 0] = 1.6603723851E-09;
			K[1, 1] = 2.4255934705E-09;
			K[2, 2] = 2.8783795974E-09;
			K[3, 3] = 3.1052955589E-09;
			K[4, 4] = 3.1910050747E-09;
			K[5, 5] = 3.1940896540E-09;
			K[6, 6] = 3.1503514453E-09;
			K[7, 7] = 3.0804600582E-09;
			K[8, 8] = 2.9960384237E-09;
			K[9, 9] = 2.9035548030E-09;
		}

		private static void Run(double x, double y, double eps)
		{
			Matrix.Matrix s, S1, S2, k1, M, L, dR, dQ, Tmp;
			double d1, d2;
			int kh = 0;

			s = new Matrix.Matrix(10, 1);
			S1 = new Matrix.Matrix(10, 2);
			S2 = new Matrix.Matrix(10, 2);
			M = new Matrix.Matrix(10, 2);
			dR = new Matrix.Matrix(10, 1);

			k1 = ~K; kh = 0;

			do
			{
				num_du_rungekutt(f, g, x, y, 0, ref s);
				num_du_rungekutt(f, g, x + d, y, 0, ref S1);
				num_du_rungekutt(f, g, x - d, y, 0, ref S2);
				num_du_rungekutt(f, g, x, y + d, 1, ref S1);
				num_du_rungekutt(f, g, x, y - d, 1, ref S2);
				M = (S1 - S2) / (2 * d);
				L = !M;
				dR = R - s;

				Tmp = L * k1;
				dQ = (~(Tmp * M)) * Tmp * dR;

				d1 = dQ[0, 0]; d2 = dQ[1, 0];
				x += d1; y += d2;

				kh++;
				Console.WriteLine(kh + " : x1 = " + x + "  x2 = " + y);

			} while ((Math.Abs(d1) > eps) && (Math.Abs(d2) > eps));
		}

		public static void Main(string[] args)
		{
			Init();

			Console.Write("x01: ");
			double x01 = Convert.ToDouble(Console.ReadLine());

			Console.Write("x02: ");
			double x02 = Convert.ToDouble(Console.ReadLine());

			Console.Write("eps: ");
			double eps = Convert.ToDouble(Console.ReadLine());

			Run(x01, x02, eps);

			Console.Write("\nThe end");
			Console.ReadKey();
		}
	}
}