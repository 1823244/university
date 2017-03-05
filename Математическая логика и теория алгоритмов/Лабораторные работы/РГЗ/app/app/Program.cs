using System;
using System.Collections.Generic;
using RPNLib;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            ReversePolishNotation notation = new ReversePolishNotation();

            Console.Write("Okay, dude. Enter the expression, ok?\n\t");
            //notation.Expression = Console.ReadLine();
            notation.Expression = "A & B + A & !B & C + A & !B & !C + A & !C";
            int number = notation.GetNumberIdentifiers();
            int rows = (int)Math.Pow(2, number);
            bool[] values = new bool[number];
            Constituents constituents = new Constituents(number);

            for (int i = 0; i < rows; i++)
            {
                string binString = Convert.ToString(i, 2).PadLeft(number, '0');
                int sum = 0;

                for (int j = 0; j < number; j++)
                {
                    if (Equals(binString[j], '1'))
                    {
                        values[j] = true;
                        sum++;
                    }
                    else
                    {
                        values[j] = false;
                    }
                }

                notation.SetValues(values);

                if (notation.Result() == "true")
                {
                    bool?[] constituent = new bool?[number];
                    for (int j = 0; j < number; j++)
                    {
                        constituent[j] = values[j];
                    }
                    constituents.Add(sum, constituent);
                }
            }

            if (constituents.Length() == rows)
            {
                Console.WriteLine("1");
                Console.ReadKey();
                return;
            }

            if (constituents.Length() == 0)
            {
                Console.WriteLine("0");
                Console.ReadKey();
                return;
            }

            List<bool?[]> answer = constituents.GetAnswer();
            foreach (bool?[] item in answer)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == null) continue;
                    if (item[i] == true)
                    {
                        Console.Write("x{0}", i + 1);
                    } else if (item[i] == false) {
                        Console.Write("!x{0}", i + 1);
                    }
                    Console.Write(" & ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }

    class Constituents
    {
        private List<bool?[]>[] constituents;
        private int number;
        private int cols;

        public Constituents(int number)
        {
            constituents = new List<bool?[]>[number + 1];
            for (int i = 0; i < number + 1; i++)
            {
                constituents[i] = new List<bool?[]>();
            }
            this.number = number;
            cols = (int)Math.Pow(2, number);

        }

        public int Length()
        {
            int counter = 0;
            foreach (List<bool?[]> item in constituents)
            {
                counter += item.Count;
            }
            return counter;
        }

        public void Add(int field, bool?[] constituent)
        {
            constituents[field].Add(constituent);
        }

        private static bool Contains(List<bool?[]> list, bool?[] el)
        {
            foreach (bool?[] item in list)
            {
                bool flag = true;
                for (int i = 0; i < item.GetLength(0); i++)
                {
                    if (item[i] != el[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return true;
                }
            }

            return false;
        }

        private void Merge()
        {
            bool howlong = true;
            while (howlong)
            {
                howlong = false;

                bool[] checkerFirst;
                bool[] checkerSecond = new bool[constituents[0].Count];

                for (int i = 0; i < constituents.Length - 1; i++)
                {
                    checkerFirst = checkerSecond;
                    checkerSecond = new bool[constituents[i + 1].Count];
                    List<bool?[]> tmp = new List<bool?[]>();

                    for (int j = 0; j < constituents[i].Count; j++)
                    {
                        for (int k = 0; k < constituents[i + 1].Count; k++)
                        {
                            int counter = 0;
                            bool?[] tmpElement = new bool?[number];
                            for (int l = 0; l < number; l++)
                            {
                                if (constituents[i][j][l] == constituents[i + 1][k][l])
                                {
                                    tmpElement[l] = constituents[i][j][l];
                                }
                                else if ((constituents[i][j][l] != null) && (constituents[i + 1][k][l] != null))
                                {
                                    tmpElement[l] = null;
                                    counter++;
                                }
                                else
                                {
                                    counter = 2;
                                    break;
                                }
                            }
                            if (counter == 1)
                            {
                                checkerFirst[j] = true;
                                checkerSecond[k] = true;

                                if (!Contains(tmp, tmpElement))
                                {
                                    tmp.Add(tmpElement);
                                    howlong = true;
                                }
                            }
                        }
                    }

                    for (int j = 0; j < constituents[i].Count; j++)
                    {
                        if (!checkerFirst[j])
                        {
                            tmp.Add(constituents[i][j]);
                        }
                    }
                    constituents[i] = tmp;

                    if (i == constituents.Length - 2)
                    {
                        for (int j = 0; j < constituents[i + 1].Count; j++)
                        {
                            if (checkerSecond[j])
                            {
                                constituents[i + 1].RemoveAt(j);
                            }
                        }
                    }
                }
            }
        }

        public List<bool?[]> GetAnswer()
        {
            Merge();
            List<bool?[]> answer = new List<bool?[]>();
            foreach (List<bool?[]> constituent in constituents)
            {
                if (constituent.Count != 0)
                {
                    foreach (bool?[] item in constituent)
                    {
                        answer.Add(item);
                    }
                }
            }

            bool[,] matrix = new bool[answer.Count, cols];

            for (int i = 0; i < cols; i++)
            {
                string binString = Convert.ToString(i, 2).PadLeft(number, '0');
                for (int j = 0; j < answer.Count; j++)
                {
                    bool checker = true;
                    for (int k = 0; k < number; k++)
                    {
                        if (((answer[j][k] == true) && (binString[k] == '0')) || ((answer[j][k] == false) && (binString[k] == '1'))) {
                            checker = false;
                            break;
                        }
                    }
                    if (checker)
                    {
                        matrix[j, i] = true;
                    }
                }
            }

            bool[] needed = new bool[answer.Count];
            for (int i = 0; i < cols; i++)
            {
                int counter = 0;
                int saved = 0;
                for (int j = 0; j < answer.Count; j++)
                {
                    if (matrix[j, i])
                    {
                        counter++;
                        saved = j;
                    }
                }
                if (counter == 1)
                {
                    needed[saved] = true;
                }
            }

            for (int i = answer.Count - 1; i != 0; i--)
            {
                if (!needed[i])
                {
                    answer.RemoveAt(i);
                }
            }

            return answer;
        }
    } 
}
