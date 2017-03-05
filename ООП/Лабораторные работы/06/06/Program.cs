using System;
using System.Threading;

namespace OOP_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator  = ".";

            Point point = new Point();
            Console.Write("Координаты после инициализации: ");
            point.Print();

            point = new Point(5, 10);
            Console.Write("Координаты после инициализации значениями 5 и 10: ");
            point.Print();

            Console.WriteLine("Расстояние от 0,0 до заданной точки: {0:N2}", point.Vector());

            Console.Write("Координаты точки после перемещения на вектор (3, 7): ");
            point.Transition(3, 7);
            point.Print();

            Console.Write("Координаты точки после задания x = 1, y = 2: ");
            Point.Coords tmp;
            tmp.x = 1;
            tmp.y = 2;
            point.Coordinates = tmp;
            point.Print();

            Console.Write("И после их умножения на 10: ");
            point.Multiplication = 10;
            point.Print();

            Console.ReadKey();
        }
    }

    public class Point
    {
        /// <summary>
        /// Структура для хранения координат точки
        /// </summary>
        public struct Coords
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Поле для выполнения операций с элементом класса — точкой
        /// </summary>
        private Coords coordinates;
        
        /// <summary>
        /// Конструктор класса, задающий нулевые значения
        /// </summary>
        public Point()
        {
            coordinates.x = 0;
            coordinates.y = 0;
        }

        /// <summary>
        /// Конструктор класса, с заданными координатами
        /// </summary>
        /// <param name="a">x</param>
        /// <param name="b">y</param>
        public Point(int a, int b)
        {
            coordinates.x = a;
            coordinates.y = b;
        }

        /// <summary>
        /// Выводит координаты точки на экран
        /// </summary>
        public void Print()
        {
            Console.WriteLine("({0}; {1})", coordinates.x, coordinates.y);
        }

        /// <summary>
        /// Находит расстояние от начала координат до точки
        /// </summary>
        /// <returns>Длину вектора от 0,0 до x,y</returns>
        public double Vector()
        {
            return Math.Sqrt(coordinates.x * coordinates.x + coordinates.y * coordinates.y);
        }

        /// <summary>
        /// Перемещает точку на плоскости на вектор (a, b)
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        public void Transition(int a, int b)
        {
            coordinates.x += a;
            coordinates.y += b;
        }

        /// <summary>
        /// Позволяет задать или получить координаты
        /// </summary>
        public Coords Coordinates
        {
            get { return coordinates; }
            set { coordinates = value; }
        }

        /// <summary>
        /// Позволяет умножить координаты на скаляр (только для записи)
        /// </summary>
        public int Multiplication
        {
            set 
            { 
                coordinates.x *= value;
                coordinates.y *= value;
            }
        }
    }
}