using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace app
{
    class Board
    {
        public string[,] Data { get; set; }
        public Dictionary<string, int> NoArrive = new Dictionary<string, int>();
        public Dictionary<string, int> NoAway = new Dictionary<string, int>();
        private int count;
        private const int rows = 7;
        private const int cols = 4;
        public Board()
        {
            Data = new string[rows, cols];
            count = 0;
            ReadResources();
        }

        public void Add(string[] row)
        {
            if (count < rows)
            {
                for (int i = 0; i < cols; i++)
                {
                    Data[count, i] = row[i];
                }
                count++;
            }
        }

        public void Remove(int number)
        {
            if (number > (count - 1)) return;
            for (int i = number; i < (count - 1); i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Data[i, j] = Data[i + 1, j];
                }
            }

            for (int i = 0; i < cols; i++)
            {
                Data[count - 1, i] = "";
            }
            count--;
        }

        public void ReadResources()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Data[i, j] = "";
                }
            }

            ResXResourceReader resx = new ResXResourceReader(@"D:\Dropbox\БГТУ\ООП\РГЗ\app\app\DataTrains.resx");

            foreach (DictionaryEntry entry in resx)
            {
                string[] tokens = ((string)entry.Value).Split('#');
                string[] row = new string[cols] { (string)entry.Key, tokens[0], tokens[1], tokens[2]};
                Add(row);
            }
        }

        public int Check(string time)
        {
            for (int i = 0; i < rows; i++)
            {
                if (Data[i, 1] == time)
                {
                    Random random = new Random();
                    int rand = random.Next(10);
                    if (rand > 4)
                    {
                        NoArrive.Add(Data[i, 0], random.Next(10) + 2);                       
                    }
                    return i + 1;
                }
                else if (Data[i, 3] == time)
                {
                    Random random = new Random();
                    int rand = random.Next(10);
                    if (rand > 4)
                    {
                        NoAway.Add(Data[i, 0], random.Next(10) + 2);
                    }
                    return -(i + 1);
                }
            }
            return 0;
        }
    }
}
