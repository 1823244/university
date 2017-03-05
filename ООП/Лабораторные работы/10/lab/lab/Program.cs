using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture =
                            new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture
                               .NumberFormat.NumberDecimalSeparator = ".";

            Console.Write("Введите количество записей: ");
            int n = Convert.ToInt32(Console.ReadLine());
            List list = new List();
            for (int i = 0; i < n; i++)
            {
                Student tmp = new Student();
                Console.Write("{0})   Имя: ", i + 1);
                tmp.Name = Console.ReadLine();

                Console.Write("     Группа: ");
                tmp.Group = Convert.ToInt32(Console.ReadLine());

                Console.Write("     Пять оценок через пробел: ");
                string[] marksString = Console.ReadLine().Split(' ');
                int[] marks = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    marks[j] = Int32.Parse(marksString[j]);
                }
                tmp.Marks = marks;

                list.Insert(list.SearchIndex(tmp), tmp);
            }

            Console.Clear();
            foreach (Student tmp in list)
            {
                Console.WriteLine(tmp.ToString());
            }

            Console.Write("\nВывести список студентов с оценкой более: ");
            double mark = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            list.Search(mark);
            Console.ReadKey();
        }
    }
}
