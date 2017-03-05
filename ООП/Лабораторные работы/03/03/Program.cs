using System;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter n, please: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] A;
            int[] ArrayOfMax;
            InputMatrix(out A, n);
            FindMaxInRows(A, out ArrayOfMax, n);
            OutputArray(ArrayOfMax, n);
        }

        static void InputMatrix(out int[,] A, int n)
        {
            A = new int[n, n];
            Console.WriteLine("Enter nxn matrix's elements one by one, please:");
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Console.Write("\t{0}, {1}: ", i + 1, j + 1);
                    A[i, j] = Convert.ToInt32(Console.ReadLine());
                }
        }

        static void FindMaxInRows(int[,] A, out int[] ArrayOfMax, int n)
        {
            ArrayOfMax = new int[n];
            int max;

            for (int i = 0; i < n; i++)
            {
                max = A[i, 0];
                for (int j = 1; j < n; j++)
                {
                    if (A[i, j] > max)
                    {
                        max = A[i, j];
                    }
                }
                ArrayOfMax[i] = max;
            }
        }

        static void OutputArray(int[] ArrayOfMax, int n)
        {
            Console.WriteLine("Array of maximum elements in rows:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("\t{0}: {1}", i + 1, ArrayOfMax[i]);
            }
        }
    }
}