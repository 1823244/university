using System;

namespace Matrix
{

    //матрица
    public class Matrix
    {
        private double[,] _matr;
        private int _m, _n;
        public const double eps = 0.000000001;

        public Matrix(int m, int n)
        {
            this._m = m;
            this._n = n;
            _matr = new double[m, n];
        }

        public Matrix(double[,] initmatr)
        {
            _m = initmatr.GetLength(0);
            _n = initmatr.GetLength(1);
            _matr = new double[_m, _n];
            for (int i = 0; i < _m; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    _matr[i, j] = initmatr[i, j];
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if ((i >= _m) || (j >= _n) || (i < 0) || (j < 0)) throw new IndexOutOfRangeException();
                return _matr[i, j];
            }
            set
            {
                if ((i >= _m) || (j >= _n) || (i < 0) || (j < 0)) throw new IndexOutOfRangeException();
                this._matr[i, j] = value;
            }
        }

        /// <summary>Строк в матрице </summary>
        public int m
        {
            get { return this._m; }
            private set { }
        }

        /// <summary>Столбцов в матрице </summary>
        public int n
        {
            get { return this._n; }
            private set { }
        }

        /// <summary>
        /// Сложить матрицы
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix result;
            if ((A.m != B.m) || (A.n != B.n)) throw new IndexOutOfRangeException();

            result = new Matrix(A.m, B.n);
            
            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < A.n; j++ )
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }

            return result;
        }
        
        /// <summary>
        /// вычесть матрицы
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            Matrix result;
            if ((A.m != B.m) || (A.n != B.n)) throw new IndexOutOfRangeException();

            result = new Matrix(A.m, B.n);
            
            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < A.n; j++ )
                {
                    result[i, j] = A[i, j] - B[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// умножить матрицы
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix result;
            if (A.n != B.m) throw new IndexOutOfRangeException();

            result = new Matrix(A.m, B.n);

            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < B.n; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < A.n; k++)
                    {
                        result[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return result;
        }

        //прямой ход
        private static int _directflow(Matrix LPart, Matrix RPart)
        {
            bool extended_matr;     //есть ли правая часть
            double MaxAbsElement;   //максимальный по модулю элемент в столбце
            int MaxAbsElementID;    //номер строки с сабжем выше
            double cooficient;      //коофициент/разрешающий элемент
            double tmp;             //буфер
            int switches = 0;       //количество перестановок строк

            if ((LPart != null) && (LPart.m == LPart.n))
            {
                extended_matr = (RPart != null);
                for (int i = 0; i < LPart.m; i++)
                {
                    
                    //частичный выбор главного элемента
                    //поиск максимального элемента в i-том столбце, начиная с i-той позиции
                    MaxAbsElement = 0;
                    MaxAbsElementID = 0;
                    for (int j = i; j < LPart.m; j++)
                    {
                        if (Math.Abs(LPart[j, i]) > MaxAbsElement)
                        {
                            MaxAbsElement = Math.Abs(LPart[j, i]);
                            MaxAbsElementID = j;
                        }
                    }

                    //если максимальный по модулю 0
                    if (MaxAbsElement < eps) return -1;
                    
                    //если строка с максимальным элементом стоит не на нужной диагонали
                    if (i != MaxAbsElementID)
                    {
                        //перестановка строк detected
                        switches++;

                        //для левой части есть смысл копировать с i-того столбца (левее нули)
                        for (int k = i; k < LPart.n; k++)
                        {
                            tmp = LPart[i, k];
                            LPart[i, k] = LPart[MaxAbsElementID, k];
                            LPart[MaxAbsElementID, k] = tmp;
                        }

                        //меняем строки расширенной части матрицы если надо
                        if (extended_matr) for (int k = 0; k < RPart.n; k++)
                        {
                            tmp = RPart[i, k];
                            RPart[i, k] = RPart[MaxAbsElementID, k];
                            RPart[MaxAbsElementID, k] = tmp;
                        }
                        
                    }
                    
                    //убираем Xi из текущего столбца
                    for (int k = i + 1; k < LPart.n; k++)
                    {
                        //вычисляем разрещающий коофициент
                        cooficient = -1 * LPart[k, i] / LPart[i, i];
                        //текущий элемент 0
                        LPart[k, i] = 0;
                        //проходим матрицу
                        for (int j = i + 1; j < LPart.m; j++) LPart[k, j] += LPart[i, j] * cooficient;
                        //проходим расширенную матрицу  
                        if (extended_matr) for (int j = 0; j < RPart.n; j++) RPart[k, j] += RPart[i, j] * cooficient;
                    }
                } //обработка столбцов
                return switches;
            }
            else
            {
                return -1;
            }
        }

        //обратный ход
        private static int _reverseflow(Matrix LPart, Matrix Rpart)
        {
            if ((LPart == null) || (Rpart == null)) return 1;
            //для всех строк матрицы
            for (int i = LPart.m - 1; i >= 0; i--)
            {
                //делим на разрешающий элемент
                for (int j = 0; j < Rpart.n; j++) Rpart[i, j] /= LPart[i, i];
                //вычитаем из вышестоящих строк текущую, умноженную на элемент, стоящий в этой строке и в столбце с разрешающим элементом
                for (int j = i - 1; j >= 0; j--)
                {
                    for (int k = 0; k < Rpart.n; k++) Rpart[j, k] -= Rpart[i, k] * LPart[j, i];
                }
            }
            return 0;
        }

        /// <summary>
        /// Получить единичную матрицу
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Matrix E(int size)
        {
            Matrix result;
            if (size > 0)
            {
                result = new Matrix(size, size);
                //for (int i = 0; i < size; i++) for (int j = 0; j < size; j++) result[i, j] = 0;
                for (int i = 0; i < size; i++) result[i, i] = 1;
                return result;
            }
            else throw new OperationCanceledException();
        }

        /// <summary>
        /// получить определитель
        /// </summary>
        public double Determinant
        {
            get
            {
                if ((_m == _n) && (_m != 0))
                {
                    Matrix _ = new Matrix(_matr);
                    double result;
                    result = (_directflow(_, null) % 2) == 1 ? -1 : 1;
                    for (int i = 0; i < _n; i++) result *= _[i, i];
                    return result;
                }
                else throw new OperationCanceledException();
            }
            private set { }
        }

        /// <summary>
        /// обратная матрицв
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix operator ~ (Matrix A)
        {
            if (A._m == A._n)
            {
                Matrix _E = E(A._m);
                return SolveSLAU(A, _E);
            }
            else throw new OperationCanceledException();
        }

        /// <summary>
        /// Решить СЛАУ
        /// </summary>
        /// <param name="LPart"></param>
        /// <param name="RPart"></param>
        /// <returns></returns>
        public static Matrix SolveSLAU(Matrix LPart, Matrix RPart)
        {
            if ((LPart == null) || (RPart == null)) throw new OperationCanceledException();
            Matrix _L = new Matrix(LPart._matr), _R = new Matrix(RPart._matr);
            if (_directflow(_L, _R) == -1) throw new OperationCanceledException();
            _reverseflow(_L, _R);
            return _R;
        }

        /// <summary>
        /// Транспонирование
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix operator ! (Matrix A)
        {
            Matrix result = new Matrix(A.n, A.m);

            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    result[j, i] = A[i, j];
                }
            }

            return result;
        }
    }
}