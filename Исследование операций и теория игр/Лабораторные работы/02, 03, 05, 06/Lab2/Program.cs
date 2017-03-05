using System;
using System.IO;
using FractionNameSpace;

namespace Lab2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            #region Потоки
            //StreamReader streamReader = new StreamReader("in2.txt");
            //StreamWriter streamWriter = new StreamWriter("out2.txt");
            
            //StreamReader streamReader = new StreamReader("in3.txt");
            //StreamWriter streamWriter = new StreamWriter("out3.txt");

            //StreamReader streamReader = new StreamReader("in5.txt");
            //StreamWriter streamWriter = new StreamWriter("out5.txt");

            StreamReader streamReader = new StreamReader("in6.txt");
            StreamWriter streamWriter = new StreamWriter("out6.txt");
            #endregion

            //SimplexTable table = new SimplexTable(streamReader);
            //table.PrintSimplexTable(streamWriter);

            //Симплекс-метод
            //table.SimplexMethod (streamWriter);

            //Метод больших штрафов
            //table.MSolution(streamWriter);

            //Метод искусственного базиса
            //table.MethodArtificialBasis(streamWriter);

            //Двойственный метод
            //table.DvoistvMethod(streamWriter);

            //Игры
            StreamWriter streamWriter2 = new StreamWriter("tmp6.txt");
            convert(streamReader, streamWriter2);
            streamWriter2.Close();
            streamReader.Close();
            streamReader = new StreamReader("tmp6.txt");
            SimplexTable table = new SimplexTable(streamReader);
            table.DvoistvMethodForGame(streamWriter);

            #region Потоки
            streamReader.Close();
            streamWriter.Close();
            #endregion
        }

        public static void convert(StreamReader inp, StreamWriter outp)
        {
            int m, n;
            string[] line = inp.ReadLine().Split(' ');
            m = Int32.Parse(line[0]);
            n = Int32.Parse(line[1]);
            outp.WriteLine("{0} {1}", m, n);

            for (int i = 0; i <= m; i++)
            {
                outp.Write("{0}", 0);
                if (i != n)
                {
                    outp.Write(" ");
                }
            }
            outp.WriteLine();
            
            Fraction[,] table = new Fraction[n, m];

            Fraction min = new Fraction(1);

            for (int i = 0; i < n; i++)
            {
                string l = inp.ReadLine();

                if (l != null && l.Length > 0)
                {
                    string[] part = l.Split(' ');

                    for (int j = 0; j < m; j++)
                    {
                        if (part[j].Contains("/"))
                        {
                            string[] tmp = part[j].Split('/');
                            table[i, j] = new Fraction(Int32.Parse(tmp[0]), Int32.Parse(tmp[1]));
                        }
                        else
                        {
                            table[i, j] = new Fraction(Int32.Parse(part[j]));
                        }

                        if (table[i, j] < min)
                        {
                            min = table[i, j];
                        }
                    }
                }
            }

            if (min != 1)
            {
                min *= -1;
                Fraction d = new Fraction(1);
                min += d;

                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        table[i, j] += min;
                    }
                }
            }

            for (int i = 0; i < table.GetLength(1); i++)
            {
                outp.Write("1 ");
                for (int j = 0; j < table.GetLength(0); j++)
                {
                    outp.Write("{0} ", (String)table[j, i]);
                }
                outp.WriteLine();
            }

            outp.Write("{0} ", 0);
            for (int i = 0; i < n; i++)
            {
                outp.Write("{0}", -1);
                if (i != n - 1)
                {
                    outp.Write(" ");
                }
            }
            outp.WriteLine();
        }
    }
}
