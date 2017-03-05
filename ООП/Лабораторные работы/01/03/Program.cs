using System;
using System.Threading;

namespace LabOnePartThree
{
    class LabOnePartThree
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Console.Write("Enter a2, a1 and b separated by space, please: ");
            string[] num = Console.ReadLine().Split(' ');
            int a2 = int.Parse(num[0]);
            int a1 = int.Parse(num[1]);
            int b = int.Parse(num[2]);

            Console.WriteLine("Tens digit = {1}, ones digit = {0}", (a1 + b) % 10, a2 + (int)((a1 + b) / 10));
            Console.ReadKey();
        }
    }
}