using System;
using System.Collections.Generic;
using System.IO;

namespace _01
{
    public class Logic : Matr
    {
        // Fields
        double[,] basic;
        
        // Constructs
        public Logic(int h, int w)
            : base(h, w)
        {
            basic = null;
        }

        public Logic(double[,] m)
            : base(m)
        {
            basic = null;
        }

        public Logic(Logic m)
            : base(m)
        {
            basic = m.Basic;
        }
        
        // Properties
        public List<int> BasicIndexesUnknown
        {
            get { return BasicUnknown(basic); }
        }
        
        public double[,] Basic
        {
            get { return basic; }
        }

        public int Rang
        {
            get
            {
                if (basic != null) { return basic.GetLength(0); }
                else { return 0; }
            }
        }
        
        // Methods
        public void PrintInit(StreamWriter stream)
        {
            PrintMatrix(stream);
        }

        public void PrintBasic(StreamWriter stream)
        {
            Print(basic, stream);
        }
        
        public double[,] BasicForm()
        {
            basic = BasicForm(matrix, 0);
            return basic;
        }

        public double[,] BasicForm(int[] combinations)
        {
            basic = DelZeroLines(basic);
            if (basic == null) { return null; }

            List<int> usedLines = new List<int>();
            double allowElement;

            basic = (double[,])matrix.Clone();

            foreach (int col in combinations)
            {
                if (col > basic.GetLength(1) - 1)
                {
                    basic = null;
                    break;
                }

                int row = 0;
                while ((usedLines.Contains(row) || basic[row, col] == 0) && row < basic.GetLength(0))
                {
                    row++;
                }

                if (row >= matrix.GetLength(0)) { return null; }

                usedLines.Add(row);
                allowElement = basic[row, col];

                for (int i = 0; i < basic.GetLength(1); i++)
                {
                    basic[row, i] /= allowElement;
                }

                for (int i = 0; i < basic.GetLength(0); i++)
                {
                    if (i == row) { continue; }

                    for (int j = 0; j < basic.GetLength(1); j++)
                    {
                        if (j == col) { continue; }
                        basic[i, j] = basic[i, j] - basic[i, col] * basic[row, j];
                    }
                    basic[i, col] = 0;
                }
            }
            basic = DelZeroLines(basic);

            return basic;
        }

        public List<double[,]> AllBasicForms(StreamWriter stream)
        {
            if (Rang == 0) { return null; }
            List<double[,]> lst = new List<double[,]>();

            Combinations(ref lst, new int[Rang], 0, 0, matrix.GetLength(1) - 1, stream);

            return lst;
        }

        public double[,] ReplacementOperation(UInt32 xi, UInt32 xj)
        {
            xi--;
            xj--;
            if (basic == null || xi >= basic.GetLength(1) - 1 || xj >= basic.GetLength(1) - 1) { return null; }

            int row = -1;
            for (int i = 0; i < basic.GetLength(0); i++)
            {
                if (row == -1 && basic[i, xi] == 1) { row = i; }
                else if (row != -1 && basic[i, xi] == 1) { return null; }
            }

            if (basic[row, xj] == 0) { return null; }
            
            double allowElement = basic[row, xj];
            
            for (int i = 0; i < basic.GetLength(1); i++)
            {
                basic[row, i] = basic[row, i] / allowElement;
            }

            for (int i = 0; i < basic.GetLength(0); i++)
            {
                if (i == row) { continue; }
                
                for (int j = 0; j < basic.GetLength(1); j++)
                {
                    if (j == xj) { continue; }
                    basic[i, j] = basic[i, j] - basic[i, xj] * basic[row, j];
                }
                basic[i, xj] = 0;
            }

            return basic;
        }

        private double[,] BasicForm(double[,] m, int row)
        {
            m = DelZeroLines(m);
            if (m == null) { return null; }
            
            if (row >= m.GetLength(0)) { return m; }

            int col = 0;
            double allowElement = m[row, col];
            while (allowElement == 0 && col < m.GetLength(1) - 1)
            {
                allowElement = m[row, ++col];
            }
            
            if (col == m.GetLength(1) - 1) { return BasicForm(m, row + 1); }

            for (int i = 0; i < m.GetLength(1); i++)
            {
                m[row, i] = m[row, i] / allowElement;
            }
            
            for (int i = 0; i < m.GetLength(0); i++)
            {
                if (i == row) { continue; }

                for (int j = 0; j < m.GetLength(1); j++)
                {
                    if (j == col) { continue; }
                    
                    m[i, j] = m[i, j] - m[i, col] * m[row, j];
                }
                
                m[i, col] = 0;
            }

            if (m == null) { return null; }
            return BasicForm(m, row + 1);
        }
        
        private double[,] DelZeroLines(double[,] m)
        {
            int count = 0;
            bool[] flags = new bool[m.GetLength(0)];

            for (int i = 0; i < m.GetLength(0); i++)
            {
                flags[i] = true;
                for (int j = 0; j < m.GetLength(1) - 1; j++)
                {
                    if (Math.Abs(0 - m[i, j]) > 0.0000001)
                    {
                        count++;
                        flags[i] = false;
                        break;
                    }
                }
         
                if (flags[i] == true && Math.Abs(0 - m[i, m.GetLength(1) - 1]) > 0.0000001) { return null; }
            }
            
            double[,] tmp = new double[count, m.GetLength(1)];
            for (int i = 0, t = 0; i < tmp.GetLength(0); i++)
            {
                if (flags[i]) { continue; }

                for (int j = 0; j < m.GetLength(1); j++)
                {
                    tmp[t, j] = m[i, j];
                }
                t++;
            }

            return tmp;
        }
        
        private void Combinations(ref List<double[,]> allBasicForms, int[] init, int fill, int minValue, int n,
                                   StreamWriter stream)
        {
            int end = n - init.Length + fill + 1;
            for (int x = minValue; x < end; x++)
            {
                init[fill] = x;
                if (fill == init.Length - 1)
                {
                    allBasicForms.Add(BasicForm(init));
                    if (stream != null)
                    {
                        stream.Write("\nБазисный вид для базовых переменных");
                        foreach (int a in init)
                        {
                            stream.Write(" x{0}", a + 1);
                        }
                        stream.WriteLine(": ");
                        PrintBasic(stream);
                    }
                }
                else
                {
                    Combinations(ref allBasicForms, init, fill + 1, x + 1, n, stream);
                }
            }
        }


        public static List<double[,]> AllBasicSolutions(List<double[,]> basicForms, StreamWriter stream)
        {
            if (basicForms == null) { return null; }

            List<double[,]> result = new List<double[,]>();

            foreach (double[,] m in basicForms)
            {
                bool flag = false;
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    if (m[i, m.GetLength(1) - 1] < 0)
                    {
                        flag = true;
                        break;
                    }
                }
                
                if (flag) { continue; }
                
                result.Add(m);
                if (stream != null)
                {
                    List<int> lst = BasicUnknown(m);
                    string str = "";
                    foreach (int a in lst)
                    {
                        str += "x" + (a + 1) + " ";
                    }
                    stream.WriteLine("\nОдно из опорных решений при базисных неизвестных {0}", str);
                    PrintArray(m, stream);
                }
            }

            return result;
        }
        
        public static List<int> BasicUnknown(double[,] m)
        {
            if (m == null) { return null; }
            else
            {
                List<int> lst = new List<int>();

                int countOnes;
                int countAll;

                for (int col = 0; col < m.GetLength(1) - 1; col++)
                {
                    countOnes = countAll = 0;
                    for (int line = 0; line < m.GetLength(0); line++)
                    {
                        if (m[line, col] == 1)
                        {
                            countAll++;
                            countOnes++;
                        }
                        else if (m[line, col] != 0) { countAll++; }
                    }
                    
                    if (countOnes == 1 && countOnes == countAll) { lst.Add(col); }
                }
                return lst;
            }
        }
        
        public static void PrintArray(double[,] m, StreamWriter stream)
        {
            if (m == null)
            {
                Console.WriteLine("NullPointer");
                return;
            }

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write("{0, 7:f2} ", m[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}