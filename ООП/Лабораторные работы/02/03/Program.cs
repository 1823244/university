using System;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 22; i += 2)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}