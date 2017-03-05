using System;

namespace Labs
{
    class MainClass
    {
        //Лаб 1
        public static double fLab1(double x)
        {
            return Math.Pow(Math.Log(x), 2);
        }
        //Лаб 3
        public static double fLab3_1(double x)
        {
            return Math.Sin(0.7 * x + 0.4) / (2.2 + Math.Cos(0.3 * x * x + 0.7));
        }

        public static double fLab3_2(double x)
        {
            return x * x * Math.Log10(x);
        }

        public static double fLab3_3(double x)
        {
            return Math.Cos(3 * x);
        }
        //Лаб 4
        public static double fLab4_1(double x, double y)
        {
            //y'
            return (x * Math.Sin(x) - y) / x;
        }

        public static double fLab4_RealSolution(double x)
        {
            return (Math.Sin(x) - 1) / x - Math.Cos(x);
        }
        //Лаб 9
        public static void MatrixOperationWork(double[,] a, double[,] b)
        {
            double[,] c = MatrixOperations.Multiplication(a, a);
            double[,] d = MatrixOperations.Multiplication(b, -1);
            c = MatrixOperations.Addition(c, d);
            c = MatrixOperations.Multiplication(c, a);

            d = MatrixOperations.Addition(d, b);

            double[,] e = MatrixOperations.Addition(b, a);
            e = MatrixOperations.Multiplication(e, 2);
            e = MatrixOperations.Multiplication(e, b);
            e = MatrixOperations.Multiplication(e, -1);

            c = MatrixOperations.Addition(c, e);

            Console.WriteLine("Решение задачи с матрицами:");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write("{0,6}", c[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void Lab9()
        {
            //Произвести операции с матрицами
            double[,] A = {
                {2, 3, 1},
                {-1, 2, 4},
                {5, 3, 0}
			};
            double[,] B = {
                {2, 7, 13},
                {-1, 0, 5},
                {5, 13, 21}
			};
            MatrixOperationWork(A, B);

            double[,] C = {
				{4, 5, -2},
                {3, -1, 0},
                {4, 2, 7}
			};
            double[,] obrMatr = MatrixMethodGauss.ObrMatr(C);
            Console.WriteLine("\nОбратная матрица:");
            for (int i = 0; i < obrMatr.GetLength(0); i++)
            {
                for (int j = 0; j < obrMatr.GetLength(1); j++)
                {
                    Console.Write("{0,9}", Math.Round(obrMatr[i, j], 4));
                }
                Console.WriteLine();
            }

            double[,] D = {
                {1, 2, 4},
                {5, 1, 2}, 
                {3, -1, 1}
                //{1, 1, -1, -1},
                //{0, 1, 2, -1},
                //{1, -1, 0, -1},
                //{-1, 3, -2, 0}
			};
            double[,] n = {
                {31},
                {29},
                {10}
                //{0},
                //{2},
                //{-1},
                //{0}
			};

            obrMatr = MatrixMethodGauss.SystemSolution(D, n);
            Console.WriteLine("\nРешение системы:");
            for (int i = 0; i < obrMatr.GetLength(0); i++)
            {
                Console.Write("{0,6}", obrMatr[i, 0]);
            }
            Console.ReadLine();
        }

        public static void Main(string[] args)
        {
            Lab9();

            #region lab10
            //Вычисление собственных чисел и собственных векторов матрицы
            //double epsilon = 0.01;
            //double[] X = { 1, 0, 0 };
            //double[,] A = {
            //    { 3.3, 1, 2.3 },
            //    { 1, 3.8, 2.3 },
            //    { 2.3, 2.3, 4.3  }
            //};
            //EigenValuesVectors.MaxValue(A, X, epsilon);
            //Console.ReadLine();

            #endregion
        }
    }
}
