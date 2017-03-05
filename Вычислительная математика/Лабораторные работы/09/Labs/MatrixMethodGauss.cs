using System;

namespace Labs
{
    public static class MatrixMethodGauss
    {
        //Решение системы a - коэффициенты b - свободные члены (задается матрицей)
        //Если передать свободными членами обратную матрицу, то результатом получим обратную матрицу
        public static double[,] SystemSolution(double[,] a, double[,] b)
        {
            int k = 0;
            double[,] result = new double[a.GetLength(0), a.GetLength(1) + b.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (j >= a.GetLength(1))
                    {
                        result[i, j] = b[i, j - a.GetLength(1)];
                    }
                    else
                    {
                        result[i, j] = a[i, j];
                    }
                }
            }

            for (int i = 0; i < result.GetLength(0) - 1; i++)
            {
                //Упорядочиваем элементы в i столбце
                Upor(ref result, ref k, i, i);
                NullStolb(ref result, i, i);
            }

            int indexLastCol = a.GetLength(1) - 1;
            for (int j = a.GetLength(1); j < result.GetLength(1); j++)
            {
                for (int i = result.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int ik = a.GetLength(1) - 1; ik > i; ik--)
                    {
                        result[i, j] -= result[i, ik] * b[ik, j - a.GetLength(1)];
                    }
                    if (result[i, i] == 0)
                    {
                        Console.WriteLine("Error");
                        return null;
                    }
                    else
                    {
                        b[i, j - a.GetLength(1)] = result[i, j] / result[i, i];
                    }
                }
            }

            return b;
        }
        public static double[,] ObrMatr(double[,] a)
        {
            double[,] oneMatr = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < oneMatr.GetLength(0); i++)
            {
                for (int j = 0; j < oneMatr.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        oneMatr[i, j] = 1;
                    }
                    else
                    {
                        oneMatr[i, j] = 0;
                    }
                }
            }

            return SystemSolution(a, oneMatr);
        }
        //Упорядочить j столбец, верхней строкой считать line
        private static void Upor(ref double[,] a, ref int k, int line, int j)
        {
            for (int i = line; i < a.GetLength(0) - 1; i++)
            {
                if (a[i, j] < a[i + 1, j])
                {
                    Stroki(ref a, i, i + 1);
                    k++;
                    i = line - 1;
                }
            }
        }
        //Поменять строки местами
        private static void Stroki(ref double[,] a, int i1, int i2)
        {
            double c;
            for (int j = 0; j < a.GetLength(1); j++)
            {
                c = a[i1, j];
                a[i1, j] = a[i2, j];
                a[i2, j] = c;
            }
        }
        //Нулевые элементы под a[i,j]
        private static void NullStolb(ref double[,] a, int i, int j)
        {
            double tmp;
            for (int k = i + 1; k < a.GetLength(0); k++)
            {
                tmp = a[k, j] / a[i, j];
                if (tmp == 0)
                {
                    continue;
                }
                for (int l = j; l < a.GetLength(1); l++)
                {
                    a[k, l] = a[k, l] - a[i, l] * tmp;
                }
            }
        }
    }
}

