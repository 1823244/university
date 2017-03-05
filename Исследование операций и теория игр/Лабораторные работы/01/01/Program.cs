using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            StreamReader streamReader = new StreamReader("input.txt");
            StreamWriter streamWriter = new StreamWriter("result.txt");

            if (streamReader != null)
            {
                Console.SetIn(streamReader);
            }
            else
            {
                Console.WriteLine("Where is input file?");
                return;
            }

            int m = Int32.Parse(Console.ReadLine());
            int n = Int32.Parse(Console.ReadLine());

            Logic matrix = new Logic(m, n);

            matrix.Enter(streamReader);
            streamWriter.WriteLine("Исходная матрица:");
            matrix.PrintInit(streamWriter);

            streamWriter.WriteLine("\nБазисный вид:");
            matrix.BasicForm();
            matrix.PrintBasic(streamWriter);
            streamWriter.WriteLine("\nЗаменяем x2 на x3:");
            matrix.ReplacementOperation(2, 3);
            matrix.PrintBasic(streamWriter);

            streamWriter.WriteLine("\n== Все базисные виды ==");
            List<double[,]> lst = matrix.AllBasicForms(streamWriter);

            streamWriter.WriteLine("\n== Все опорные решения ==");
            List<double[,]> allBasic = Logic.AllBasicSolutions(lst, streamWriter);

            streamWriter.WriteLine("\n== Оптимальное опорное решение ==\n");
            OptimalPlan(allBasic, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }


        private static double celFunc(double[,] basicSolution)
        {
            List<int> lst = Logic.BasicUnknown(basicSolution);
            double result = 0.0;
            foreach (int stolbec in lst)
            {
                for (int line = 0; line < basicSolution.GetLength(0); line++)
                {
                    if (basicSolution[line, stolbec] == 1)
                    {
                        result += basicSolution[line, basicSolution.GetLength(1) - 1];
                        break;
                    }
                }
            }
            return result;
        }

        public static double[,] OptimalPlan(List<double[,]> allBasicSolution, StreamWriter stream)
        {
            double max = celFunc(allBasicSolution[0]);
            int index = 0;
            double tmp;
            for (int i = 1; i < allBasicSolution.Count; i++)
            {
                tmp = celFunc(allBasicSolution[i]);
                if (tmp > max)
                {
                    max = tmp;
                    index = i;
                }
            }

            if (stream != null)
            {
                stream.WriteLine("Максимальное значение целевой функции: {0}", max);
                for (int i = 0; i < allBasicSolution[index].GetLength(0); i++)
                {
                    for (int j = 0; j < allBasicSolution[index].GetLength(1); j++)
                    {
                        stream.Write("{0, 7:f2} ", allBasicSolution[index][i, j]);
                    }
                    stream.WriteLine();
                }
            }
            return allBasicSolution[index];
        }
    }
}
