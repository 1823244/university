using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace findAvg
{
    class Program
    {
        delegate double finder(double[] array, int length);

        [DllImport("findAvg.dll")]
        static extern double findAvg(double[] array, int length);

        static double findAvgCSharp(double[] array, int length)
        {
            int n = 0;
            double avg = 0;

            for (int i = 0; i < length; i++)
            {
                if (array[i] > 0)
                {
                    avg += array[i];
                    n++;
                }
            }

            if (n == 0)
            {
                return -1;
            }
            else
            {
                return avg / n;
            }
        }

        static void Test(finder f, double[] array, int length, string name)
        {
            Stopwatch timer = Stopwatch.StartNew();

            timer.Start();
            var result = f(array, length);
            timer.Stop();

            if (result == -1)
            {
                Console.WriteLine("{0} array does not contain positive elements!", name);
            }
            else
            {
                Console.WriteLine("{0} average of positive: {1:F5}", name, result);
            }

            Console.WriteLine("{0} time: {1} ms", name, timer.ElapsedMilliseconds);
        }

        static void Main(string[] args)
        {
            int length = 0;

            Console.Write("Enter length of array: ");

            try
            {
                length = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error! Length must be integer!");
                return;
            }

            double[] array = new double[length];
            var r = new Random();

            for (int i = 0; i < length; i++)
            {
                array[i] = Convert.ToDouble(r.Next(10000000) - r.Next(10000000)) / 1000;
            }

            Console.WriteLine();
            Test(findAvg, array, length, "ASM");
            
            Console.WriteLine();
            Test(findAvgCSharp, array, length, "CSharp");

            Console.ReadKey();
        }
    }
}
