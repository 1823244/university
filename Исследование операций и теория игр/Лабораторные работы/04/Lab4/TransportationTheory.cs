using System;
using System.IO;
using System.Collections.Generic;

namespace Lab4
{
    public class TransportationTheory
    {
        /*
         * Поля
        */
        int[] a;
        int[] b;
        int[,] c;
        int[,] x;
        /*
         * Конструктор
        */
        public TransportationTheory(StreamReader streamReader)
        {
            if (streamReader == null)
            {
                Console.WriteLine("streamReader == null!");
                return;
            }

            string[] tmpStr;

            tmpStr = streamReader.ReadLine().Split(' ');
            a = new int[tmpStr.Length];
            for (int i = 0; i < tmpStr.Length; i++)
            {
                a[i] = Int32.Parse(tmpStr[i]);
            }

            tmpStr = streamReader.ReadLine().Split(' ');
            b = new int[tmpStr.Length];
            for (int i = 0; i < tmpStr.Length; i++)
            {
                b[i] = Int32.Parse(tmpStr[i]);
            }

            c = new int[a.Length, b.Length];
            x = new int[a.Length, b.Length];

            for (int i = 0; i < a.Length; i++)
            {
                tmpStr = streamReader.ReadLine().Split(' ');
                for (int j = 0; j < b.Length; j++)
                {
                    c[i, j] = Int32.Parse(tmpStr[j]);
                }
            }
        }
        /*
         * Методы
         * public
        */
        //Нахождение первого опорного плана
        public void FirstSupportPlan()
        {
            int[] a1 = new int[a.Length];
            a.CopyTo(a1, 0);
            int[] b1 = new int[b.Length];
            b.CopyTo(b1, 0);

            //Метод северо-западного угла
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (a1[i] == -1)
                    {
                        //Строка вычеркнута
                        break;
                    }
                    else if (b1[j] == -1)
                    {
                        //Столбец вычернут
                        continue;
                    }

                    if (a1[i] > b1[j])
                    {
                        //Вычеркиваем столбец
                        a1[i] -= b1[j];
                        x[i, j] = b1[j];
                        b1[j] = -1;
                    }
                    else if (a1[i] < b1[j] || a1[i] == b1[j])
                    {
                        //Вычеркиваем строку
                        x[i, j] = a1[i];
                        b1[j] -= a1[i];
                        a1[i] = -1;
                        break;
                    }
                }
            }
        }
        //Распределительный метод
        public void DistributionMethod(StreamWriter streamWriter)
        {
            //Находим первый опорный план
            FirstSupportPlan();
            streamWriter.WriteLine("Первый опорный план");
            PrintMatr(streamWriter);

            List<Tre> listLoop = new List<Tre>();

            //Проходимся по всей матрице X и для пустых клеток составляем цикл пересчета
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (x[i, j] == 0)
                    {
                        listLoop.Add(new Tre(i, j, 1));
                        Loop(ref listLoop);
                        int g = Gamma(listLoop);
                        if (g < 0)
                        {
                            //Находим в клетках с отрицательным знаком минимальный элемент
                            Tre min = listLoop[1];
                            for (int ij = 3; ij < listLoop.Count; ij += 2)
                            {
                                if (x[min.I, min.J] > x[listLoop[ij].I, listLoop[ij].J])
                                {
                                    min = listLoop[ij];
                                }
                            }
                            //streamWriter.Write("min = {0}. ", x[min.I, min.J]);
                            //Производим сдвиг по циклу пересчета
                            int tmp = x[min.I, min.J];
                            for (int ij = 0; ij < listLoop.Count; ij++)
                            {
                                x[listLoop[ij].I, listLoop[ij].J] += tmp * listLoop[ij].Zn;
                            }
                            //Проверяем матрицу X заново
                            i = j = 0;
                            streamWriter.Write("Произвели сдвиг по циклу [g = {0}]: ", g);
                            for (int ij = 0; ij < listLoop.Count; ij++)
                            {
                                streamWriter.Write("[{0}, {1}]", listLoop[ij].I + 1, listLoop[ij].J + 1);
                            }
                            streamWriter.WriteLine();
                            PrintMatr(streamWriter);
                        }
                        listLoop.RemoveRange(0, listLoop.Count);
                    }
                }
            }
            //PrintMatr(streamWriter);
            int summ = 0;
            //Вычисляем значение целевой функции
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    summ += c[i, j] * x[i, j];
                }
            }
            streamWriter.WriteLine("Z = {0}", summ);
        }
        //Вывод матрицы
        public void PrintMatr(StreamWriter streamWriter)
        {
            streamWriter.Write("{0,10}", "A\\B");
            for (int i = 0; i < b.Length; i++)
            {
                streamWriter.Write("{0,10}", b[i]);
            }
            streamWriter.WriteLine();

            for (int i = 0; i < a.Length; i++)
            {
                streamWriter.Write("{0,10}", a[i]);
                for (int j = 0; j < b.Length; j++)
                {
                    streamWriter.Write("{0,10}", "[" + c[i, j] + "]" + x[i, j]);
                }
                streamWriter.WriteLine();
            }
            streamWriter.WriteLine();
        }
        /*
         * private
        */
        private class Tre
        {
            public Tre(int i, int j, int zn)
            {
                this.I = i;
                this.J = j;
                this.Zn = zn;
            }

            public int I { get; set; }

            public int J { get; set; }

            public int Zn { get; set; }
        }
        //Строим цикл пересчета
        private void Loop(ref List<Tre> listLoop)
        {
            //Берем самую левую точку в строке с элементом а[0]
            for (int j = 0; j < b.Length; j++)
            {
                if (x[listLoop[0].I, j] == 0)
                {
                    continue;
                }
                //Как встретили базовую переменную продолжаем составлять цикл
                listLoop.Add(new Tre(listLoop[0].I, j, -1));
                if (Step(ref listLoop, true))
                {
                    return;
                }
                listLoop.RemoveRange(1, listLoop.Count - 1);
            }
            //Берем самую верхнюю точку в столбце с элементом а[0]
            for (int i = 0; i < a.Length; i++)
            {
                if (x[i, listLoop[0].J] == 0)
                {
                    continue;
                }
                //Как встретили базовую переменную продолжаем составлять цикл
                listLoop.Add(new Tre(i, listLoop[0].I, -1));
                if (Step(ref listLoop, false))
                {
                    return;
                }
                listLoop.RemoveRange(1, listLoop.Count - 1);
            }
        }
        //Делаем шаги влево/вправо или вверх/вниз и ищем оставшиеся вершины цикла
        private bool Step(ref List<Tre> listLoop, bool UpDown)
        {
            //Если точек больше 3, то проверяем, если последний и первый элемент находятся в одной строке
            //и можно ходить в сторону, то цикл найден(или тоже самое для столбца)
            if (listLoop.Count > 3 &&
                ((listLoop[listLoop.Count - 1].I == listLoop[0].I && !UpDown) || (listLoop[listLoop.Count - 1].J == listLoop[0].J && UpDown)))
            {
                return true;
            }

            //В зависимости от UpDown ищем базовый элемент в строке или столбце
            if (UpDown)
            {
                int j = listLoop[listLoop.Count - 1].J;
                for (int i = 0; i < a.Length; i++)
                {
                    if (x[i, j] == 0 || i == listLoop[listLoop.Count - 1].I)
                    {
                        continue;
                    }
                    //Как встретили базовую переменную продолжаем составлять цикл
                    listLoop.Add(new Tre(i, j, listLoop[listLoop.Count - 1].Zn * (-1)));
                    if (Step(ref listLoop, !UpDown))
                    {
                        return true;
                    }
                    listLoop.RemoveAt(listLoop.Count - 1);
                }
            }
            else
            {
                int i = listLoop[listLoop.Count - 1].I;
                for (int j = 0; j < b.Length; j++)
                {
                    if (x[i, j] == 0 || j == listLoop[listLoop.Count - 1].J)
                    {
                        continue;
                    }
                    //Как встретили базовую переменную продолжаем составлять цикл
                    listLoop.Add(new Tre(i, j, listLoop[listLoop.Count - 1].Zn * (-1)));
                    if (Step(ref listLoop, !UpDown))
                    {
                        return true;
                    }
                    listLoop.RemoveAt(listLoop.Count - 1);
                }
            }
            return false;
        }
        //Вычисляем значение Гамма
        private int Gamma(List<Tre> listLoop)
        {
            int summ = 0;

            for (int i = 0; i < listLoop.Count; i++)
            {
                summ += c[listLoop[i].I, listLoop[i].J] * listLoop[i].Zn;
            }

            return summ;
        }
    }
}

