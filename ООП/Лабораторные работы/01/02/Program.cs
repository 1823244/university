using System;
using System.Threading;

namespace LabOnePartTwo
{
    class LabOnePartTwo
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");

            Console.Write("Enter n, please: ");

            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("x = {0}", (n % 100) * 10 + (int)(n / 100));
            Console.ReadKey();
        }
    }
}