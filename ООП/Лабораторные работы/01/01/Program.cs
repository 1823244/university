using System;
using System.Threading;

namespace LabOnePartOne
{
    class LabOnePartOne
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator  = ".";

            double a, b, denominatorA;

            Console.Write("Enter x, y and z separated by spaces, please: ");
            string[] num = Console.ReadLine().Split(' ');
            double x = double.Parse(num[0]);
            double y = double.Parse(num[1]);
            double z = double.Parse(num[2]);

            denominatorA = 1 / 3;

            a = (2 * Math.Pow(Math.Sin(x), 2 * denominatorA) - 0.0015 * y) / (denominatorA += Math.Exp(-2 * z));
            b = x - z * z / 5 + denominatorA;

            Console.WriteLine("a = {0:N2}, b = {1:N2}", a, b);
            Console.ReadKey();
        }
    }
}