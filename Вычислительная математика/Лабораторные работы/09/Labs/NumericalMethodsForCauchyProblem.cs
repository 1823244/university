using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labs
{
	//Лабораторная работа 4
    class NumericalMethodsForCauchyProblem
    {
        private const int GRAN = 15;

        public delegate double Func(double x, double y);
        public delegate double[,] Func2(Func f, double a, double b, double y0, double h);

        public static double[,] createEulerTable(Func f, double a, double b, double y0, double h)
        {
            //число шагов
            int n = (int)((b - a) / h);
            double[,] table = new double[2, n+1];
            table[0, 0] = a;    //x0
            table[1, 0] = y0;   //y(x0);

            for (int i = 1; i <= n; i++)
            {
                table[0, i] = table[0, i - 1] + h;
                table[1, i] = table[1, i - 1] + h * f(table[0, i - 1], table[1, i - 1]);
            }
            return table;
        }
        public static double[,] createEulerKoshiTable(Func f, double a, double b, double y0, double h)
        {
            //число шагов
            int n = (int)((b - a) / h);
            double[,] table = new double[2, n + 1];
            table[0, 0] = a;    //x0
            table[1, 0] = y0;   //y(x0);

            for (int i = 1; i <= n; i++)
            {
                table[0, i] = table[0, i - 1] + h;
                table[1, i] = table[1, i - 1] + h / 2 * (f(table[0, i - 1], table[1, i - 1]) + f(table[0, i - 1] + h, table[1, i - 1] + h * f(table[0, i - 1], table[1, i - 1])));
                
            }
            return table;
        }
        public static double[,] createModifEulerTable(Func f, double a, double b, double y0, double h)
        {
            //число шагов
            int n = (int)((b - a) / h);
            double[,] table = new double[2, n + 1];
            table[0, 0] = a;    //x0
            table[1, 0] = y0;   //y(x0);

            for (int i = 1; i <= n; i++)
            {
                table[0, i] = table[0, i - 1] + h;
                table[1, i] = table[1, i - 1] + h * f(table[0, i - 1] + h / 2, table[1, i - 1] + h / 2 * f(table[0, i - 1], table[1, i - 1]));
            }
            return table;
        }
        public static double[,] createRungeKuttaTable(Func f, double a, double b, double y0, double h)
        {
            //число шагов
            int n = (int)((b - a) / h);
            double[,] table = new double[2, n + 1];
            table[0, 0] = a;    //x0
            table[1, 0] = y0;   //y(x0);

            double m1, m2, m3, m4;

            for (int i = 1; i <= n; i++)
            {
                m1 = f(table[0, i - 1],
                    table[1, i - 1]);
                m2 = f(table[0, i - 1] + h / 2,
                    table[1, i - 1] + h / 2 * m1);
                m3 = f(table[0, i - 1] + h / 2,
                    table[1, i - 1] + h / 2 * m2);
                m4 = f(table[0, i - 1] + h,
                    table[1, i - 1] + h * m3);
                table[0, i] = table[0, i - 1] + h;
                table[1, i] = table[1, i - 1] + h / 6 * (m1 + 2 * m2 + 2 * m3 + m4);
            }
            return table;
        }

        public static double[,] Method(Func f, Func2 f2, double a, double b, double h, double y0)
        {
            if (f2 == createEulerTable)
            {
                Console.WriteLine("Метод Эйлера");
            }
            else if (f2 == createEulerKoshiTable)
            {
                Console.WriteLine("Метод Эйлера-Коши");
            }
            else if (f2 == createModifEulerTable)
            {
                Console.WriteLine("Модифицированный Метод Эйлера");
            }
            else
            {
                Console.WriteLine("Метод Рунге-Кутта");
            }
            double[,] tableH = f2(f, a, b, y0, h);
            return tableH;
        }
        public static double[,] Method(Func f, Func2 f2, double a, double b, double h, double y0, double eps)
        {
            double maxPogr = 0;
            double[,] tableH = f2(f, a, b, y0, h);
            int count = 0;
            do
            {
                count++;
                h = h / 2;
                maxPogr = 0;
                double[,] tableH2 = f2(f, a, b, y0, h);

                double tmp;
                for (int i = 0; i < tableH.GetLength(1); i++)
                {
                    tmp = Math.Abs(tableH[1, i] - tableH2[1, 2 * i]);
                    if (f2 == createEulerTable)
                    {
                        tmp /= (Math.Pow(2, 1) - 1);
                        Console.WriteLine("Макс. погрешность: {0:f4}\n Шаг: {1:f4}");
                    }
                    else if (f2 == createEulerKoshiTable || f2 == createModifEulerTable)
                    {
                        tmp /= (Math.Pow(2, 2) - 1);
                    }
                    else
                    {
                        tmp /= (Math.Pow(2, 4) - 1);
                    }
                    if (tmp > maxPogr) { maxPogr = tmp; }
                }
                tableH = tableH2;
            } while (maxPogr > eps && count < GRAN);

            if (f2 == createEulerTable)
            {
                Console.WriteLine("Метод Эйлера");
            }
            else if (f2 == createEulerKoshiTable)
            {
                Console.WriteLine("Метод Эйлера-Коши");
            }
            else if (f2 == createModifEulerTable)
            {
                Console.WriteLine("Модифицированный Метод Эйлера");
            }
            else
            {
                Console.WriteLine("Метод Рунге-Кутта");
            }

            Console.WriteLine("Макс. погрешность: {0:f4}\n Шаг: {1:f4}", maxPogr, h);
            return tableH;
        }
    }
}
