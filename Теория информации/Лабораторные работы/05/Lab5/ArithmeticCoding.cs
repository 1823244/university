using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4
{
    public class ArithmeticCoding
    {
        /*
         * Поля
        */
        Dictionary<string, decimal> dictionaryWithAlfavit;
        Dictionary<string, decimal> dictionaryOfCumulativeProbabilities;
        List<string> listKeys;
        /*
         * Конструкторы
        */
        public ArithmeticCoding(StreamReader stream)
        {
            dictionaryWithAlfavit = new Dictionary<string, decimal>();
            dictionaryOfCumulativeProbabilities = null;
            if (stream != null)
            {
                EnterAlfavit(stream);
            }
        }
        /*
         * Свойства
        */
        public Dictionary<string, decimal> DictionaryWithAlfavit
        {
            get
            {
                return this.dictionaryWithAlfavit;
            }
        }

        public Dictionary<string, decimal> DictionaryOfCumulativeProbabilities
        {
            get
            {
                return this.dictionaryOfCumulativeProbabilities;
            }
        }
        /*
         * Методы
         * public
        */
        //Ввод первоначального алфавита(с размером блока считающимся равным 1)
        public void EnterAlfavit(StreamReader stream)
        {
            dictionaryOfCumulativeProbabilities = new Dictionary<string, decimal>();
            listKeys = new List<string>();
            string read = stream.ReadLine();
            string[] tmp = null;
            decimal comulativeProbabilities = 0;

            while (read != null && read.Contains(" "))
            {
                tmp = read.Split(' ');
                if (tmp.Length > 1)
                {
                    decimal value;
                    if (!Decimal.TryParse(tmp[1], out value))
                    {
                        Console.WriteLine("Ошибка в файле!\nВещественные числа должны быть запсианы в виде 0,14");
                        return;
                    }
                    else
                    {
                        //Добавляем вероятность символа
                        dictionaryWithAlfavit.Add(tmp[0], value);
                        listKeys.Add(tmp[0]);
                        //вычисляем и добавляем его комулятивную вероятность
                        dictionaryOfCumulativeProbabilities.Add(tmp[0], comulativeProbabilities);
                        comulativeProbabilities += value;
                    }
                }
                read = stream.ReadLine();
            }
        }
        //Вывести в поток алфавит(словарь символ - вероятность)
        public void PrintDictionary(Dictionary<string, decimal> dictionary, StreamWriter stream)
        {
            if (dictionary != null && dictionary.Count > 0 && stream != null)
            {
                foreach (KeyValuePair<string, decimal> pair in dictionary)
                {
                    stream.WriteLine("{0} {1}", pair.Key, pair.Value);
                }
            }
            else
            {
                Console.WriteLine("Ошибка вывода!");
            }
        }
        //Кодирование сообщения
        public void EncodingMessage(string message, StreamWriter streamW)
        {
            decimal F = 0;
            decimal G = 1;

            for (int i = 0; i < message.Length; i++)
            {
                F = F + G * dictionaryOfCumulativeProbabilities["" + message[i]];
                G = G * dictionaryWithAlfavit["" + message[i]];
            }

            int k = (int)Math.Ceiling(-Math.Log((double)G, 2)) + 1;
            decimal C = Round(F + G / 2, message.Length);
            string binary = DoubleToBin(C, k);

            string tmp = C.ToString().Split(',')[1];
            int length = tmp.Length;
            while (tmp[length - 1] == '0')
            {
                length--;
            }

            decimal Z = Round(BinToDouble(binary), length);


            streamW.WriteLine("Сообщение: {0}\nC: {1}\nk: {2}\nZ: {3}", binary, C, k, Z.ToString().Substring(0, length + 2));
        }
        //Декодирование сообщения
        public void DecodingMessage(StreamReader streamR, StreamWriter streamW)
        {
            if (streamR != null && streamW != null)
            {
                string result = "";

                string tmp = streamR.ReadLine();
                string number = streamR.ReadLine().Split(' ')[1];
                decimal F = Decimal.Parse(number);

                decimal S = 0;
                decimal G = 1;
                int n = number.Split(',')[1].Length;
                int j;

                while (n > 0)
                {
                    j = 0;
                    while (j < listKeys.Count && (S + G * dictionaryOfCumulativeProbabilities[listKeys[j]]) < F)
                    {
                        j++;
                    }
                    if (j == 0)
                    {
                        break;
                    }
                    else
                    {
                        j--;
                        S += G * dictionaryOfCumulativeProbabilities[listKeys[j]];
                        G *= dictionaryWithAlfavit[listKeys[j]];
                        result += listKeys[j];
                    }
                    n--;
                }

                streamW.WriteLine(result);
            }
        }
        /*
         * private
        */
        //Округление числа с заданной точностью
        private static decimal Round(decimal value, int digits)
        {
            decimal scale = (decimal)Math.Pow(10.0, digits);
            decimal round = Math.Floor(Math.Abs(value) * scale + 0.5m);
            return (Math.Sign(value) * round / scale);
        }
        //Перевод вещественного числа к двоичному виду
        private string DoubleToBin(decimal number, int len)
        {
            string str = Convert.ToString((int)number, 2) + ".";
            number = number - (int)number;
            int c;
            int n = 0;
            while (n < len)
            {

                number *= 2;
                c = Convert.ToInt32(Math.Truncate(number));
                str = String.Concat(str, Convert.ToString(c));
                number -= c;
                n++;
            }
            return str;
        }
        //Перевод вещественного числа из двоичной системы
        private decimal BinToDouble(string binaryDouble)
        {
            decimal res = 0;
            string[] tmp = binaryDouble.Split('.');
            int ras = tmp[0].Length - 1;

            //переводим целую часть 
            for (int i = 0; i < tmp[0].Length; i++)
            {
                if (tmp[0][ras--] == '1')
                {
                    res += (decimal)Math.Pow(2, i);
                }
            }
            //переводим вещественную часть
            for (int i = 1; i <= tmp[1].Length; i++)
            {
                if (tmp[1][i - 1] == '1')
                {
                    res += (decimal)Math.Pow(2, -i);
                }
            }

            return res;
        }
    }
}

