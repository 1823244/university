using System;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the string, please: ");
            string s = Console.ReadLine();
            Console.Write("Enter the <x>: ");
            string x = Console.ReadLine().Substring(0, 1);
            Console.Write("Enter the <y>: ");
            string y = Console.ReadLine().Substring(0, 1);

            s = s.Replace(x, x + y);
            Console.WriteLine("Result: {0}", s);
        }
    }
}