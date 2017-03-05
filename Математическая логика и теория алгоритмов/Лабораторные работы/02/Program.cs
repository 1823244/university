using System;
using RPNLib;

namespace ML_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ReversePolishNotation notation = new ReversePolishNotation();

            Console.Write("Okay, dude. Enter expression, ok?\n\t");
            notation.Expression = Console.ReadLine(); // input expression;

            int number = notation.GetNumberIdentifiers();
            int rows = (int)Math.Pow(2, number);
            bool[] values = new bool[number];
            bool valid = true;
            string PCNF = "", PDNF = "";

            string[] identifiers = notation.GetIdentifiers();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\t");
            foreach (string identifier in identifiers)
            {
                Console.Write(" {0} ", identifier);
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("| F |");
            Console.ResetColor();
            Console.Write("\t");

            for (int i = 0; i < number; i++)
            {
                Console.Write("---");
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-----");
            Console.ResetColor();

            for (int i = 0; i < rows; i++)
            {
                string BinString = Convert.ToString(i, 2).PadLeft(number, '0');

                Console.Write("".PadLeft(4));
                if (i < 10) 
                {
                    if (rows > 9)
                    {
                        Console.Write("0");
                    }
                    else 
                    {
                        Console.Write(" ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("{0} |", i);
                Console.ResetColor();

                for (int j = 0; j < number; j++ )
                {
                    if (Equals(BinString[j], '1'))
                    {
                        values[j] = true;
                    }
                    else
                    {
                        values[j] = false;
                    }
                    Console.Write(" {0} ", BinString[j]);
                }

                notation.SetValues(values);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (notation.Result() == "true")
                {
                    Console.WriteLine("| 1 |");
                    PDNF += '(';
                    for (int j = 0; j < number; j++ )
                    {
                        if (values[j])
                        {
                            PDNF += identifiers[j] + " & ";
                        }
                        else
                        {
                            PDNF += "!" + identifiers[j] + " & ";
                        } 
                    }

                    PDNF = PDNF.Substring(0, PDNF.Length - 3) + ") + ";
                }
                else
                {
                    Console.WriteLine("| 0 |");
                    valid = false;

                    PCNF += '(';
                    for (int j = 0; j < number; j++)
                    {
                        if (values[j])
                        {
                            PCNF += "!" + identifiers[j] + " + ";
                        }
                        else
                        {
                            PCNF += identifiers[j] + " + ";
                        }
                    }

                    PCNF = PCNF.Substring(0, PCNF.Length - 3) + ") & ";
                }
                Console.ResetColor();
            }
            
            if (valid)
            {
                Console.WriteLine("\n\tValid formula.");
            }
            else
            {
                Console.WriteLine("\n\tNot valid formula.");
            }

            PCNF = PCNF.Substring(0, PCNF.Length - 3);
            PDNF = PDNF.Substring(0, PDNF.Length - 3);

            Console.WriteLine("PCNF: {0}", PCNF);
            Console.WriteLine("PDNF: {0}", PDNF);

            Console.ReadKey();
        }
    }
}