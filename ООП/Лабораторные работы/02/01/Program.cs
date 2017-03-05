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

            Console.Write("Okay, dude. Enter the first number: ");
            double first = Convert.ToDouble(Console.ReadLine());
            Console.Write("And the second, please: ");
            double second = Convert.ToDouble(Console.ReadLine());

            double epsilon = 0.00001;
            if ((first - second) > epsilon)
            {
                Console.WriteLine("Maximum = {0:N2}", first);
            }
            else if ((second - first) > epsilon)
            {
                Console.WriteLine("Maximum = {0:N2}", second);
            }
            else
            {
                Console.WriteLine("Numbers equal or too high computation error.");
            }

            Console.ReadKey();
        }
    }
}