using System;
using System.Threading;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Console.Write("Enter n, please: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] A;
            InputMatrix(out A, n);
            FindAverange(A, n);
        }

        static void InputMatrix(out int[,] A, int n)
        {
            A = new int[n, n];
            Console.WriteLine("Enter nxn matrix's elements one by one, please:");
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Console.Write("\t{0}, {1}: ", i + 1, j + 1);
                    A[i, j] = Convert.ToInt32(Console.ReadLine());
                }
        }

        static void FindAverange(int[,] A, int n)
        {
            int sum = 0, count = 0;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if ((A[i, j] % 2) != 0)
                    {
                        sum += A[i, j];
                        count++;
                    }
                }

            if (count != 0)
            {
                Console.WriteLine("Averange: {0:N2}", (double)sum / count);
            }
            else
            {
                Console.WriteLine("Don't have odd elements.");
            }
        }
    }
}