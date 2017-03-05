using System;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter n, please: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Number of maximum elements: {0}", FindMaxCount(n));
        }
       
        static int FindMaxCount(int n)
        {
            int tmp, count = 1;

            Console.WriteLine("Enter {0} elements: ", n);
            Console.Write("\t1: ");
            int max = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i < n; i++)
            {
                Console.Write("\t{0}: ", i + 1);
                tmp = Convert.ToInt32(Console.ReadLine());

                if (tmp > max)
                {
                    max = tmp;
                    count = 1;
                }
                else if (tmp == max)
                {
                    count++;
                }
            }

            return count;
        }
    }
}