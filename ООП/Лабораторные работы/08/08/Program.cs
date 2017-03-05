using System;
using System.Threading;

namespace OOP_08
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Point point = new Point(3, 2);

            Console.Write("Новая точка: ");
            point.Print();

            point++;
            Console.Write("После инкремента: ");
            point.Print();

            point--;
            Console.Write("После декремента: ");
            point.Print();

            point = point + 2;
            Console.Write("Координаты + 2: ");
            point.Print();

            point = 3 + point;
            Console.Write("3 + координаты: ");
            point.Print();

            Console.Write("X = Y? ");
            if (point)
            {
                Console.WriteLine("Да.");
            }
            else
            {
                Console.WriteLine("Нет.");
            }

            Console.WriteLine("Неявное преобразование: ");
            string s = "  Точка: " + point;
            Console.WriteLine(s);

            Console.WriteLine("Явное преобразование (\"1 2\" в Point): ");
            Console.Write("  Точка: ");
            s = "1 2";
            point = (Point)s;
            point.Print();

            Console.ReadKey();
        }
    }

    public class Point
    {
        private int x;
        private int y;

        // Конструктор класса, задающий нулевые значения
        public Point()
        {
            x = 0;
            y = 0;
        }

        // Конструктор класса, с заданными координатами
        public Point(int a, int b)
        {
            x = a;
            y = b;
        }

        // Выводит координаты точки на экран
        public void Print()
        {
            Console.WriteLine("({0}; {1})", x, y);
        }

        // Находит расстояние от начала координат до точки
        public double Vector()
        {
            return Math.Sqrt(x * x + y * y);
        }

        // Перемещает точку на плоскости на вектор (a, b)
        public void Transition(int a, int b)
        {
            x += a;
            y += b;
        }

        // Позволяет задать или получить координату X
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        // Позволяет задать или получить координату Y
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        // Позволяет умножить координаты на скаляр (только для записи)
        public int Multiplication
        {
            set
            {
                x *= value;
                y *= value;
            }
        }

        // Сравнивает поля X и Y
        private static bool IsEqual(Point value)
        {
            return value.x == value.y;
        }

        // Возвращает точку сдвинутую на вектор rhs
        private static Point Add(Point lhs, int rhs)
        {
            return new Point(lhs.x + rhs, lhs.y + rhs);
        }

        public static Point operator ++(Point value)
        {
            value.Transition(1, 1);
            return value;
        }

        public static Point operator --(Point value)
        {
            value.Transition(-1, -1);
            return value;
        }

        public static bool operator true(Point value)
        {
            return IsEqual(value);
        }

        public static bool operator false(Point value)
        {
            return !IsEqual(value);
        }

        public static Point operator +(Point lhs, int rhs)
        {
            return Add(lhs, rhs);
        }

        public static Point operator +(int lhs, Point rhs)
        {
            return Add(rhs, lhs);
        }

        public static implicit operator string(Point point)
        {
            return String.Format("({0}; {1})", point.x.ToString(), point.y.ToString());
        }

        public static explicit operator Point(string s)
        {
            string[] splitted = s.Split(' ');
            return new Point(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]));
        }
    }
}