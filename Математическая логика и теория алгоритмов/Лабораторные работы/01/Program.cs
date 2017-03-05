using System;
using RPNLib;

namespace ML_1
{
    class ML_1
    {
        static void Main(string[] args)
        {
            ReversePolishNotation notation = new ReversePolishNotation();

            Console.Write("Okay, dude. Enter expression, ok?\n\t");
            notation.Expression = Console.ReadLine(); // input expression;

            Console.WriteLine("\nReverse Polish Notation: \n\t{0}", notation.Expression);

            notation.InputValues();
            Console.WriteLine("\nResult: {0}", notation.Result());
            Console.ReadKey();
        }
    }
}