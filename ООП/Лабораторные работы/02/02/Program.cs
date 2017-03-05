using System;
using System.Threading;

namespace Application
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Console.WriteLine("Okay, dude. Let's do it! Enter coordinates:");
            Console.Write("\tx: ");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.Write("\ty: ");
            double y = Convert.ToDouble(Console.ReadLine());

            if (y >= 0)
            {
                double vector = y * y + x * x;
                if (vector < 9)
                {
                    Console.WriteLine("Point is in area.");
                }
                else if (vector == 9)
                {
                    Console.WriteLine("Point is on border of area.");
                }
                else
                {
                    Console.WriteLine("Point is not in area or it's border.");
                }
            }
            else
            {
                Console.WriteLine("Point is not in area or it's border.");
            }

            Console.ReadKey();
        }
    }
}