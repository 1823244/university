using System;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the string, please: ");
            string[] stringArray = Console.ReadLine()
                .Split(new Char[] {' ', ',', '.', '!', '?', ':', ';', '—', '–'}, StringSplitOptions.RemoveEmptyEntries);
            Console.Write("Enter the substring: ");
            string substring = Console.ReadLine();

            int counter = 0;
            Console.WriteLine("Words:");
            foreach (string str in stringArray)
            {
                if (str.Contains(substring))
                {
                    counter++;
                    Console.WriteLine("\t{0}. {1}", counter, str);
                }
            }

            if (counter == 0)
            {
                Console.WriteLine("\tError! There are no words needed!");
            }

        }
    }
}