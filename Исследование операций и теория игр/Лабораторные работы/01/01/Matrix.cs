using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _01
{
    public class Matr
    {
        // Fields
        protected int height;
        protected int width;
        protected double[,] matrix;

        // Constructors
        public Matr(int h, int w)
        {
            matrix = new double[h, w];
            width = w;
            height = h;
        }

        public Matr(double[,] m)
        {
            matrix = m;
            height = m.GetLength(0);
            width = m.GetLength(1);
        }

        public Matr(Matr m)
        {
            matrix = m.Matrix;
            height = m.Height;
            width = m.Width;
        }

        // Properties
        public double[,] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        // Methods
        public void Enter(StreamReader stream)
        {
            string[] str;

            Console.SetIn(stream);

            for (int i = 0; i < height; i++)
            {
                str = Console.ReadLine().Split(' ');

                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = Double.Parse(str[j]);
                }
            }
        }

        protected void Print(double[,] m, StreamWriter stream)
        {
            Console.SetOut(stream);

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write("{0, 7:f2}", m[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void PrintMatrix(StreamWriter stream)
        {
            Print(matrix, stream);
        }
    }
}
