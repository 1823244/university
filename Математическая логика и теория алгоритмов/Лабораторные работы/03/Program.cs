using System;
using RPNLib;

namespace ML_3
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
            string PCNF = "", PDNF = "";

            string[] identifiers = notation.GetIdentifiers();

            for (int i = 0; i < rows; i++)
            {
                string BinString = Convert.ToString(i, 2).PadLeft(number, '0');

                for (int j = 0; j < number; j++)
                {
                    if (Equals(BinString[j], '1'))
                    {
                        values[j] = true;
                    }
                    else
                    {
                        values[j] = false;
                    }
                }

                notation.SetValues(values);

                if (notation.Result() == "true")
                {
                    PDNF += '(';
                    for (int j = 0; j < number; j++)
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
            }

            PCNF = PCNF.Substring(0, PCNF.Length - 3);
            PDNF = PDNF.Substring(0, PDNF.Length - 3);

            Console.WriteLine("\n\tPCNF: {0}", PCNF);
            Console.WriteLine("\tPDNF: {0}\n", PDNF);

            Console.ReadKey();
        }
    }
}