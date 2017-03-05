using System;
using System.IO;
using FractionNameSpace;

namespace Lab2
{
    public class SimplexTable
    {
        /*
         * Поля
        */
        //Симплекс таблица
        Fraction[,] table;

        //Список базисных переменных(индекс в списке = строке симплекс таблицы, значение = базисной неизвестной)
        int[] basic;
        /*
         * Конструкторы
        */
        public SimplexTable(StreamReader streamReader)
        {
            string line;
            string[] part;
            part = streamReader.ReadLine().Split(' ');
            //Количество неизвестных
            int countM = Int32.Parse(part[0]) + 1;
            //Количество базисных неизвестных + 1(целевая функция)
            int countN = Int32.Parse(part[1]) + 1;
            //Создаем матрицу
            table = new Fraction[countN, countM];
            basic = new int[countN];
            //Считываем индексы базисных переменных
            part = streamReader.ReadLine().Split(' ');
            for (int i = 0; i < basic.Length; i++)
            {
                basic[i] = Int32.Parse(part[i]);
            }
            //Считываем countN+1 строк (countN строк при базисных переменных + целевая функция)
            for (int i = 0; i < countN; i++)
            {
                line = streamReader.ReadLine();

                if (line != null && line.Length > 0)
                {
                    part = line.Split(' ');

                    for (int j = 0; j < countM; j++)
                    {
                        if (part[j].Contains("/"))
                        {
                            string[] tmp = part[j].Split('/');
                            table[i, j] = new Fraction(Int32.Parse(tmp[0]), Int32.Parse(tmp[1]));
                        }
                        else
                        {
                            table[i, j] = new Fraction(Int32.Parse(part[j]));
                        }
                    }
                }
            }
        }
        /*
         * Свойства
        */
        public Fraction[,] Table
        {
            get
            {
                return this.table;
            }
        }

        public int[] Basic
        {
            get
            {
                return this.basic;
            }
        }
        /*
         * Методы
         * public
        */
        public void PrintSimplexTable(StreamWriter streamWriter)
        {
            //Строка БП СвЧ x1 x2 x3 x4 ... xn
            streamWriter.Write("{0,10}{1,10}", "БП", "СвЧ");
            for (int i = 1; i < table.GetLength(1); i++)
            {
                streamWriter.Write("{0,10}", "x" + i);
            }
            streamWriter.WriteLine();

            for (int i = 0; i < table.GetLength(0); i++)
            {
                streamWriter.Write("{0,10}", basic[i] != 0 ? "x" + basic[i] : "z");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    streamWriter.Write("{0,10}", (String)table[i, j]);
                }
                streamWriter.WriteLine();
            }
            streamWriter.WriteLine();
        }
        //Симплекс метод(выполняем все по пунктам)
        public void SimplexMethod(StreamWriter streamWriter)
        {
            //столбец с разрешающим элементом
            int col;
            //Строка с разрешающим элементом
            int row;
            //Выполняем, пока в последней строке симплекс-таблицы есть отрицательные коэффициенты
            while (SearchNegativeRatio(out col))
            {
                if (searchElement(out row, col))
                {
                    newSimplexTable(row, col);
                    PrintSimplexTable(streamWriter);
                }
                else
                {
                    Console.WriteLine("Целевая функция неограниченна на области допустимых значений!");
                    Console.ReadLine();
                    return;
                }
            }
        }
        //Метод больших штрафов
        public void MSolution(StreamWriter streamWriter)
        {
            //Получаем множитель М
            Fraction M = MValue();

            //Сохраняем нашу симплекс таблицу
            Fraction[,] oldTable = this.table;

            //Получаем симплекс таблицу для решения задачи методом больших штрафов
            table = TableForMSolution(M);

            //Выводим полученную таблицу
            PrintSimplexTable(streamWriter);

            //Решаем задачу используя новую симплекс-таблицу
            SimplexMethod(streamWriter);

            //Возвращаем старую таблицу на ее законное место
            table = oldTable;
        }
        //Метод искусственного базиса
        public void MethodArtificialBasis(StreamWriter streamWriter)
        {
            //Сохраняем нашу симплекс таблицу
            Fraction[,] oldTable = this.table;

            //Получаем симплекс таблицу для решения задачи методом больших штрафов
            table = TableForMethodArtificialBasis();

            //Выводим полученную таблицу
            PrintSimplexTable(streamWriter);

            //Решаем задачу используя новую симплекс-таблицу
            SimplexMethod(streamWriter);

            //Решив вспомогательную систему, смотрим чему равна f
            if (table[table.GetLength(0) - 1, 0] < 0)
            {
                Console.WriteLine("Система ограничений задачи не имеет допустимого базисного вида!");
                Console.ReadLine();
            }
            else
            {
                TransformTableToFirstSimplexTable(oldTable);
                //Выводим полученную таблицу
                PrintSimplexTable(streamWriter);

                //Решаем задачу используя новую симплекс-таблицу
                SimplexMethod(streamWriter);
            }

            //Возвращаем старую таблицу на ее законное место
            table = oldTable;
        }
        //Двойственный метод
        public void DvoistvMethod(StreamWriter streamWriter)
        {
            //Строим двойственную задачу
            streamWriter.WriteLine("Строим двойственную задачу");
            Fraction[,] tableDvoistv = DV();
            int[] basicDvoistv = new int[tableDvoistv.GetLength(0)];
            PrintSimplexTable(table, streamWriter);
            PrintSimplexTable(tableDvoistv, streamWriter);

            //Уравниваем неравенства
            streamWriter.WriteLine("Уравниваем неравенства");
            newTableDV(ref table, ref basic, true);
            newTableDV(ref tableDvoistv, ref basicDvoistv, false);
            PrintSimplexTable(table, streamWriter);
            PrintSimplexTable(tableDvoistv, streamWriter);

            //Массив соответствий иксам игриков
            streamWriter.WriteLine("Массив соответствий");
            int[] sootv = new int[table.GetLength(1) - 1];
            int start = table.GetLength(0) - 1;
            for (int i = 0; i < sootv.Length; i++)
            {
                sootv[i] = start % sootv.Length;
                start++;
            }
            streamWriter.Write("{0,4}", "x");
            for (int i = 0; i < sootv.Length; i++)
            {
                streamWriter.Write("{0,4}", i + 1);
            }
            streamWriter.WriteLine();
            streamWriter.Write("{0,4}", "y");
            for (int i = 0; i < sootv.Length; i++)
            {
                streamWriter.Write("{0,4}", sootv[i] + 1);
            }
            streamWriter.WriteLine();
            streamWriter.WriteLine();

            //Решаем исходную задачу
            streamWriter.WriteLine("Решаем исходную задачу");
            PrintSimplexTable(streamWriter);
            SimplexMethod(streamWriter);

            //Выводим ответ
            streamWriter.WriteLine("Ответ");
            bool xy = false;
            for (int i = 0; i < sootv.Length; i++)
            {
                if (sootv[i] == 0)
                {
                    xy = true;
                    streamWriter.WriteLine();
                }
                if (xy)
                {
                    streamWriter.WriteLine("\ty{0} = {1}", sootv[i] + 1, (String)table[table.GetLength(0) - 1, i + 1]);
                }
                else
                {
                    int index = -1;
                    for (int j = 0; j < basic.Length; j++)
			        {
			            if (basic[j] == (i + 1))
                        {
                            index = j;
                            break;
                        }
			        }
                    streamWriter.WriteLine("\tx{0} = {1}", i + 1, index == -1 ? "0" : (String)table[index, 0]);
                }
            }

        }
        //Двойственный метод для игр
        public void DvoistvMethodForGame(StreamWriter streamWriter)
        {
            //Строим двойственную задачу
            streamWriter.WriteLine("Строим двойственную задачу");
            Fraction[,] tableDvoistv = DV();
            int[] basicDvoistv = new int[tableDvoistv.GetLength(0)];
            PrintSimplexTable(table, streamWriter);
            PrintSimplexTable(tableDvoistv, streamWriter);

            //Уравниваем неравенства
            streamWriter.WriteLine("Уравниваем неравенства");
            newTableDV(ref table, ref basic, true);
            newTableDV(ref tableDvoistv, ref basicDvoistv, false);
            PrintSimplexTable(table, streamWriter);
            PrintSimplexTable(tableDvoistv, streamWriter);

            //Массив соответствий иксам игриков
            streamWriter.WriteLine("Массив соответствий");
            int[] sootv = new int[table.GetLength(1) - 1];
            int start = table.GetLength(0) - 1;
            for (int i = 0; i < sootv.Length; i++)
            {
                sootv[i] = start % sootv.Length;
                start++;
            }
            streamWriter.Write("{0,4}", "y");
            for (int i = 0; i < sootv.Length; i++)
            {
                streamWriter.Write("{0,4}", i + 1);
            }
            streamWriter.WriteLine();
            streamWriter.Write("{0,4}", "x");
            for (int i = 0; i < sootv.Length; i++)
            {
                streamWriter.Write("{0,4}", sootv[i] + 1);
            }
            streamWriter.WriteLine(); streamWriter.WriteLine();

            //Решаем исходную задачу
            streamWriter.WriteLine("Решаем исходную задачу");
            PrintSimplexTable(streamWriter);
            SimplexMethod(streamWriter);

            //Выводим ответ
            streamWriter.WriteLine("Ответ");
            bool xy = false;
            for (int i = 0; i < sootv.Length; i++)
            {
                if (sootv[i] == 0)
                {
                    xy = true;
                    streamWriter.WriteLine();
                }
                if (xy)
                {
                    streamWriter.WriteLine("\tx{0} = {1}", sootv[i] + 1, (String)table[table.GetLength(0) - 1, i + 1]);
                }
                else
                {
                    int index = -1;
                    for (int j = 0; j < basic.Length; j++)
                    {
                        if (basic[j] == (i + 1))
                        {
                            index = j;
                            break;
                        }
                    }
                    streamWriter.WriteLine("\ty{0} = {1}", i + 1, index == -1 ? "0" : (String)table[index, 0]);
                }
            }
            streamWriter.WriteLine();

            //Находим седловую точку
            //Значение целевой функции
            Fraction ZnachCellFunc = table[table.GetLength(0) - 1, 0];
            //q (седловая точка по игрикам)
            for (int i = 0; i < table.GetLength(0) - 1; i++)
            {
                bool flag = false;
                Fraction fra = null;
                for (int j = 0; j < basic.Length; j++)
                {
                    if (basic[j] == i + 1)
                    {
                        fra = table[j, 0];
                        flag = true;
                    }
                }
                streamWriter.Write("q[{0}]={1}\t", i + 1, (!flag) ? "0" : ((string)(fra / ZnachCellFunc)));
            }
            streamWriter.WriteLine();
            //p (седловая точка по иксам)
            int jj = 1;
            for (int i = table.GetLength(1) - table.GetLength(0) + 1; i < table.GetLength(1); i++)
            {
                streamWriter.Write("p[{0}]={1}\t", jj++, (string)(table[table.GetLength(0) - 1, i] / ZnachCellFunc));
            }
            streamWriter.WriteLine();
        }
        /*
         * private
         * 1) 	Просматриваем последнюю строку симплекс-таблицы и среди коэффициентов этой строки,
         *		исключая свободный член выбираем отрицательное число.
         *
         *@out int index	если такое число найдено, запишем его индекс(номер столбца)
         *
         *Возвращает true, если такой элемент найден, false иначе
        */
        private bool SearchNegativeRatio(out int index)
        {
            index = -1;
            int row = table.GetLength(0) - 1;
            for (int i = 1; i < table.GetLength(1); i++)
            {
                if (table[row, i].Numerator < 0)
                {
                    if (index != -1)
                    {
                        if (table[row, i].Numerator < table[row, index].Numerator)
                        {
                            index = i;
                        }
                    }
                    else
                    {
                        index = i;
                    }
                }
            }

            if (index != -1) { return true; }
            return false;
        }
        /*
         * 2) 	Просматриваем i столбец таблицы.
         * 		Для каждого aij находим минимальное отношение bi/aij
         * 		Тот элемент, для которого это отношение будет минимально и
         * 		будет выбран в качестве разрешающего
         * Возвращаем true, если в столбце есть положительные элементы, false иначе
         *
        */
        private bool searchElement(out int row, int col)
        {
            //Изначально строка не выбрана
            row = -1;
            double min = 0;
            bool flag = true;
            double tmp;
            //проходимся по столбцу col 
            for (int stroka = table.GetLength(0) - 2; stroka >= 0; stroka--)
            {
                //Если элемент больше нуля
                if (table[stroka, col].Numerator > 0)
                {
                    //Находим отношение
                    Fraction t = table[stroka, 0] / table[stroka, col];
                    tmp = (double)table[stroka, 0] / (double)table[stroka, col];
                    if (flag)
                    {
                        flag = false;
                        min = tmp;
                        row = stroka;
                    }
                    else if (min >= tmp)
                    {
                        min = tmp;
                        row = stroka;
                    }
                }
            }
            return row == -1 ? false : true;
        }
        /*
         * 3) 	Строим новую симплекс таблицу
         * 		Переводим xi к свободным переменным, а xj становится базисной
        */
        private void newSimplexTable(int xi, int xj)
        {
            basic[xi] = xj;
            Fraction a;
            //Делим строку xi разрешающего элемента на него же
            a = table[xi, xj];
            for (int i = 0; i < table.GetLength(1); i++)
            {
                table[xi, i] = table[xi, i] / a;
            }
            //Проходимся по остальным строкам и отнимаем от них разрешающую
            //строку домноженную на такое число, чтобы в столбце разрешающего элемента были нули
            for (int i = 0; i < table.GetLength(0); i++)
            {
                if (i == xi)
                {
                    continue;
                }
                a = table[i, xj];
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = table[i, j] - table[xi, j] * a;
                }
            }
        }
        /*
         * Вычисляем значение М для метода больших штрафов
         * Находим максимальный коэффициент в таблице (не затрагивая свободные члены)
         * и умножаем его на 100
        */
        private Fraction MValue()
        {
            Fraction max = table[0, 1];
            //ищем по столбцам (не затрагивая 0)
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    if (max < table[i, j])
                    {
                        max = table[i, j];
                    }
                }
            }

            return max * 100;
        }
        /*
         * Составляем новую симплекс таблицу для решения задачи методом больших штрафов
         * вводим базисные переменные в уравнения, в которых их нет, вычисляем новую целевую
         * функцию
        */
        private Fraction[,] TableForMSolution(Fraction M)
        {
            //Проходимся по массиву с базисными переменными не затрагивая последний элемент(целевая функция)
            //считаем, сколько нам понадобится добавлять переменных
            int count = 0;
            for (int k = 0; k < basic.Length - 1; k++)
            {
                if (basic[k] == 0)
                {
                    count++;
                }
            }

            //Добавлять новые переменные будем начиная с номера
            int countN = table.GetLength(1);
            //Создаем таблицу нового размера, чтобы добавить новые переменные
            Fraction[,] newTable = new Fraction[table.GetLength(0), countN + count];
            //обнуляем таблицу
            for (int i = 0; i < newTable.GetLength(0); i++)
            {
                for (int j = 0; j < newTable.GetLength(1); j++)
                {
                    newTable[i, j] = new Fraction();
                }
            }

            //поднимаемся по таблице снизу
            for (int i = table.GetLength(0) - 1; i >= 0; i--)
            {
                //Копируем строку из старой таблицы
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    newTable[i, j] = table[i, j];
                }
                if (i < basic.Length - 1)
                {
                    //Если мы уже проходимся по строке с базовым элементом
                    Fraction mnojitel;

                    if (basic[i] == 0)
                    {
                        //Если не было базовой переменной, то добавляем ее и множитель будет равен М
                        basic[i] = countN;
                        newTable[i, countN] = new Fraction(1);
                        mnojitel = M;
                        countN++;
                    }
                    else
                    {
                        //Иначе, множитель будет равен коэффициенту при данной переменной в начальной целевой функции
                        mnojitel = table[table.GetLength(0) - 1, basic[i]];
                    }

                    //Умножаем строку на множитель и отнимаем от новой целевой функции
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        newTable[table.GetLength(0) - 1, j] -= mnojitel * newTable[i, j];
                    }
                }
            }

            return newTable;
        }
        /*
         * Составляем новую таблицу методом искусственного базиса 
         * для решения задачи симплекс-методом
        */
        private Fraction[,] TableForMethodArtificialBasis()
        {
            //Проходимся по массиву с базисными переменными не затрагивая последний элемент(целевая функция)
            //считаем, сколько нам понадобится добавлять переменных
            int count = 0;
            for (int k = 0; k < basic.Length - 1; k++)
            {
                if (basic[k] == 0)
                {
                    count++;
                }
            }

            //Добавлять новые переменные будем начиная с номера
            int countN = table.GetLength(1);
            //Создаем таблицу нового размера, чтобы добавить новые переменные
            Fraction[,] newTable = new Fraction[table.GetLength(0), countN + count];
            //обнуляем таблицу
            for (int i = 0; i < newTable.GetLength(0); i++)
            {
                for (int j = 0; j < newTable.GetLength(1); j++)
                {
                    newTable[i, j] = new Fraction();
                }
            }

            //поднимаемся по таблице снизу (не затрагивая строку целевой функции)
            for (int i = table.GetLength(0) - 2; i >= 0; i--)
            {
                //Копируем строку из старой таблицы
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    newTable[i, j] = table[i, j];
                }
                if (i < basic.Length - 1)
                {
                    //Если мы уже проходимся по строке с базовым элементом
                    if (basic[i] == 0)
                    {
                        //Если не было базовой переменной, то добавляем ее и отнимаем ее от нижней строки
                        basic[i] = countN;
                        newTable[i, countN] = new Fraction(1);
                        countN++;

                        for (int j = 0; j < table.GetLength(1); j++)
                        {
                            newTable[table.GetLength(0) - 1, j] -= newTable[i, j];
                        }
                    }
                }
            }

            return newTable;
        }
        /*
         * После подготовки системы ограничений, отбрасывая столбцы добавленных переменных
         * и последнюю строку получаем первую симплекс таблицу
        */
        private void TransformTableToFirstSimplexTable(Fraction[,] oldTable)
        {
            //Составляем новую таблицу размерами, как изначальная
            Fraction[,] newTable = new Fraction[oldTable.GetLength(0), oldTable.GetLength(1)];

            int celRow = newTable.GetLength(0) - 1;
            //Последнюю строку копируем из oldTable
            for (int j = 0; j < newTable.GetLength(1); j++)
            {
                newTable[celRow, j] = oldTable[celRow, j];
            }

            /* 
             * Копируем все строки кроме последней из текущей таблицы
             * и выводим их из целевой функции
            */
            for (int i = 0; i < newTable.GetLength(0) - 1; i++)
            {
                Fraction factor = newTable[celRow, basic[i]];
                for (int j = 0; j < newTable.GetLength(1); j++)
                {
                    newTable[i, j] = table[i, j];
                    newTable[celRow, j] -= newTable[i, j] * factor;
                }
            }
            table = newTable;
        }
        //Вывод таблицы
        public void PrintSimplexTable(Fraction[,] table, StreamWriter streamWriter)
        {
            //Строка БП СвЧ x1 x2 x3 x4 ... xn
            streamWriter.Write("{0,10}", "СвЧ");
            for (int i = 1; i < table.GetLength(1); i++)
            {
                streamWriter.Write("{0,10}", "x" + i);
            }
            streamWriter.WriteLine();

            for (int i = 0; i < table.GetLength(0); i++)
            {
                //streamWriter.Write ("{0,10}", basic [i] != 0 ? "x" + basic [i] : "z");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    streamWriter.Write("{0,10}", (String)table[i, j]);
                }
                streamWriter.WriteLine();
            }
            streamWriter.WriteLine();
        }
        //Строим двойственную задачу
        private Fraction[,] DV()
        {
            Fraction[,] tableDvoistv = new Fraction[this.table.GetLength(1), this.table.GetLength(0)];
            //обнуляем таблицу
            for (int i = 0; i < tableDvoistv.GetLength(0); i++)
            {
                for (int j = 0; j < tableDvoistv.GetLength(1); j++)
                {
                    tableDvoistv[i, j] = new Fraction();
                }
            }
            //Значение целевой функции
            tableDvoistv[tableDvoistv.GetLength(0) - 1, 0] = this.table[this.table.GetLength(0) - 1, 0];
            //Коэфициенты целевой функции
            for (int i = 0; i < this.table.GetLength(0) - 1; i++)
            {
                tableDvoistv[tableDvoistv.GetLength(0) - 1, 1 + i] = table[i, 0];
            }
            //Свободные члены
            for (int i = 1; i < this.table.GetLength(1); i++)
            {
                tableDvoistv[i - 1, 0] = table[table.GetLength(0) - 1, i];
            }
            //Коэффициенты
            for (int j = 0; j < tableDvoistv.GetLength(0) - 1; j++)
            {
                for (int i = 1; i < tableDvoistv.GetLength(1); i++)
                {
                    tableDvoistv[j, i] = table[i - 1, j + 1];
                }
            }

            return tableDvoistv;
        }
        //Добавляем переменные в таблицу
        private void newTableDV(ref Fraction[,] table, ref int[] basic, bool isMax)
        {
            //Проходимся по массиву с базисными переменными не затрагивая последний элемент(целевая функция)
            //считаем, сколько нам понадобится добавлять переменных
            int count = 0;
            for (int k = 0; k < basic.Length - 1; k++)
            {
                if (basic[k] == 0)
                {
                    count++;
                }
            }

            //Добавлять новые переменные будем начиная с номера
            int countN = table.GetLength(1);
            //Создаем таблицу нового размера, чтобы добавить новые переменные
            Fraction[,] newTable = new Fraction[table.GetLength(0), countN + count];
            //обнуляем таблицу
            for (int i = 0; i < newTable.GetLength(0); i++)
            {
                for (int j = 0; j < newTable.GetLength(1); j++)
                {
                    newTable[i, j] = new Fraction();
                }
            }

            //проходим по таблице
            for (int i = 0; i < table.GetLength(0); i++)
            {
                //копируем строку из старой таблицы
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    newTable[i, j] = table[i, j];
                }

                if (i < basic.Length - 1)
                {
                    if (basic[i] == 0)
                    {
                        basic[i] = countN;
                        newTable[i, countN] = new Fraction(isMax ? 1 : -1);
                        countN++;
                    }
                }
            }

            table = newTable;
        }
    }
}

