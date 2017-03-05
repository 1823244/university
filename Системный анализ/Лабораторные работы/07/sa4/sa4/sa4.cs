using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Matrix;
namespace sa4
{
    public partial class sa4 : Form
    {
        public sa4()
        {
            InitializeComponent();
            CalcEmAll();
        }

        /// <summary> количество экспериментальных данных </summary>
        public const int cValuesCount = 12;
        /// <summary> количество функций-регрессоров </summary>
        public const int cRegressorFuncCount = 6;
        /// <summary> количество дополнительных результататов опыта </summary>
        public const int cAddictionalCasesValues = 10;

        /// <summary> экспериментальные данные </summary>
        public double[,] values = new double[cValuesCount, 3]
        {
            //format: x1, x2, y
            {7.0, 2.0, 31.4568703},
            {5.0, 3.0, 30.0460275},
            {12.0, 14.0, 347.6837445},
            {11.0, 4.0, 98.2613735},
            {7.0, 3.0, 47.1486464},
            {7.0, 10.0, 147.7972654},
            {3.0, 13.0, 82.3652413},
            {14.0, 12.0, 351.5217425},
            {2.0, 11.0, 46.0615664},
            {10.0, 6.0, 140.7141210},
            {8.0, 14.0, 235.6222596},
            {9.0, 5.0, 101.5574365}
        };
        /// <summary> дополнительные результаты опыта </summary>
        public double[] addictional_cases = new double[cAddictionalCasesValues]
        {
            296.9009270,
            299.4606986,
            298.2639809,
            300.1278479,
            301.4911690,
            301.9117462,
            296.9911469,
            297.0211244,
            302.4813187,
            300.5858286
        };

        /// <summary> регрессоры </summary>
        double regressor(int iFuncID, double x1, double x2)
        {
            switch (iFuncID)
            {
                case 0: return 1;
                case 1: return x1;
                case 2: return x2;
                case 3: return x1*x2;
                case 4: return x1*x1;
                case 5: return x2*x2;
                default: return 0;
            }
        }

        string regressor_func_name(int iFuncID)
        {
            switch (iFuncID)
            {
                case 0: return " C ";
                case 1: return " Xi ";
                case 2: return " Xii ";
                case 3: return " Xi*Xii ";
                case 4: return " Xi^2 ";
                case 5: return " Xii^2 ";
                default: return " ??? ";
            }
        }

        //произвести вычисления
        public void CalcEmAll()
        {
            try
            {
                //1)строим матрицу регрессоров
                Matrix.Matrix F = new Matrix.Matrix(cValuesCount, cRegressorFuncCount);
                for (int i = 0; i < cValuesCount; i++)
                {
                    for (int j = 0; j < cRegressorFuncCount; j++)
                    {
                        F[i, j] = regressor(j, values[i, 0], values[i, 1]);
                    }
                }

                //2)вектор числовых значений эксперимента и его среднее значение
                Matrix.Matrix y = new Matrix.Matrix(cValuesCount, 1);
                double y_med = 0;
                for (int i = 0; i < cValuesCount; i++)
                {
                    y[i, 0] = values[i, 2];
                    y_med+=y[i,0];
                }
                y_med/=cValuesCount;

                //3)определяем наилучшую модель
                outbox.Text += "ПОИСК НАИЛУЧШЕЙ МАТЕМАТИЧЕСКОЙ МОДЕЛИ, ИСПОЛЬЗУЯ БАЗИСЫ:\n";
                for (int i = 0; i < cRegressorFuncCount; i++)
                {
                    outbox.Text += (i + 1).ToString() + "]" + regressor_func_name(i) + "\n";
                }
                
                List<int> RegrFuncNumbers_best=null;      //наилучший набор базисов (список их номеров из таблицы всех базисов)
                Matrix.Matrix RegrFunCoefs_best=null;     //коофициенты, при наилучшем наборе базисов
                double R_best=0;                          //коофициент значимости модели

                //все комбинации
                for (UInt32 CurrentBinSet = 1; CurrentBinSet < (1 << cRegressorFuncCount); CurrentBinSet++)
                {
                    //матрица
                    Matrix.Matrix F_local;
                    //вектор оценки неизвестных параметров модели
                    Matrix.Matrix a_local;

                    //список задействованных регрессоров в данном наборе
                    List<int> RegrFuncNumbers = new List<int>();

                    //определяем количество функций базиса
                    for (int i=0; i<32; i++) if ((CurrentBinSet & (1 << i)) != 0) RegrFuncNumbers.Add(i);

                    //задаем матрицу функций 
                    F_local = new Matrix.Matrix(cValuesCount, RegrFuncNumbers.Count);
                    for (int i = 0; i < cValuesCount; i++)
                    {
                        for (int j = 0; j < RegrFuncNumbers.Count; j++)
                        {
                            F_local[i, j] = F[i, RegrFuncNumbers[j]];
                        }
                    }

                    //матрица оценок
                    //если что, ~ - обратная матрица, ! - транспонированная матрица
                    a_local = ~(!F_local * F_local) * !F_local * y;
                    
                    //матрица значений, вычисленных по модели
                    Matrix.Matrix y_model = F_local * a_local;

                    //определяем коофициент значимости модели R
                    double Qr_local = 0;
                    for (int i = 0; i < cValuesCount; i++)
                    {
                        double _tmp = y_model[i, 0] - y_med;
                        Qr_local += _tmp * _tmp;
                    }

                    double Q_local = 0;
                    for (int i = 0; i < cValuesCount; i++)
                    {
                        double _tmp = y[i,0] - y_med;
                        Q_local += _tmp * _tmp;
                    }

                    double R_local = Qr_local / Q_local;

                    //определяем наилучшую модель
                    if (R_local > R_best)
                    {
                        //.. и запоминаем ее параметры
                        R_best = R_local;
                        RegrFuncNumbers_best = RegrFuncNumbers;
                        RegrFunCoefs_best = a_local;
                    }

                    //отчитаемся о просчитанной модели
                    outbox.Text += "\n------------------------------------------\n";
                    outbox.Text += "\nМодель №" + CurrentBinSet.ToString() + "\n";
                    outbox.Text += "Уравнение регрессии:\ny=";
                    for (int i = 0; i < RegrFuncNumbers.Count; i++, outbox.Text += RegrFuncNumbers.Count == i ? "\n" : " + ")
                    {
                        outbox.Text += a_local[i,0].ToString() + regressor_func_name(RegrFuncNumbers[i]);
                    }
                    outbox.Text += "Коэффициенты:\n";
                    outbox.Text += "\nQr = " + Qr_local.ToString();
                    outbox.Text += "\nQ  = " + Q_local.ToString();
                    outbox.Text += "\nR  = " + R_local.ToString();
                    outbox.Text += "\n------------------------------------------\n";
                }
                
                //выводим наилучшую модель
                outbox.Text += "\n=============================================\n";
                outbox.Text += "Наилучшее уравнение регрессии:\ny = ";

                for (int i = 0; i < RegrFuncNumbers_best.Count; i++, outbox.Text += RegrFuncNumbers_best.Count == i ? "\n" : " + ")
                {
                    outbox.Text += RegrFunCoefs_best[i, 0].ToString() + regressor_func_name(RegrFuncNumbers_best[i]);
                }

                outbox.Text += "Коэффициент R = " + R_best.ToString();

                //средний y (среди расчетов 13-22)
                double _My = 0;
                for (int i = 0; i < cAddictionalCasesValues; i++)
                {
                    _My += addictional_cases[i];
                }
                _My /= cAddictionalCasesValues;
                
                //оценка дисперсии шума
                double Gsqr = 0;
                for (int i = 0; i < cAddictionalCasesValues; i++)
                {
                    double _tmp = addictional_cases[i] - _My;
                    Gsqr += _tmp * _tmp;
                }
                Gsqr /= (cAddictionalCasesValues - 1); //число степеней свободы

                outbox.Text += "\n=============================================\n";
                outbox.Text += "Оценка дисперсии шума Ge^2 = " + Gsqr.ToString();
                outbox.Text += "\n\nКонец работы программы";
            }

            catch (DivideByZeroException)
            {
                outbox.Text = "Ошибка деления на 0";
            }

            catch (NullReferenceException)
            {
                outbox.Text = "Внутренняя ошибка";
            }

        }
    }
}
