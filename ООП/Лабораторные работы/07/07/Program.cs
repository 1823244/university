using System;
using System.Threading;

namespace OOP_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Console.Write("Input nummber of figures: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Figure[] n = new Figure[number];
            int i = 0;
            string[] num;
            while (i < number)
            {
                Console.Write("Choose figure type (1 — Rectangle, 2 — Circle, 3 — Triangle): ");
                int type = Convert.ToInt32(Console.ReadLine());
                switch (type)
                {
                    case 1:
                        Console.Write("Input width and height, separated by space: ");
                        num = Console.ReadLine().Split(' ');
                        int width = int.Parse(num[0]);
                        int height = int.Parse(num[1]);
                        n[i++] = new Rectangle(width, height);
                        break;
                    case 2:
                        Console.Write("Input radius: ");
                        int radius = Convert.ToInt32(Console.ReadLine());
                        n[i++] = new Circle(radius);
                        break;
                    case 3:
                        Console.Write("Input a, b, c, separated by space: ");
                        num = Console.ReadLine().Split(' ');
                        int a = int.Parse(num[0]);
                        int b = int.Parse(num[1]);
                        int c = int.Parse(num[2]);
                        n[i++] = new Triangle(a, b, c);
                        break;
                    default:
                        Console.WriteLine("Error! Try again!");
                        break;
                }
            }

            foreach (Figure figure in n)
            {
                figure.Information();
                Console.WriteLine();
            }
        }
    }

    abstract class Figure
    {
        protected Figure() { }

        public abstract double Area();
        public abstract double Perimeter();
        public abstract string Type { get; }

        public void Information()
        {
            Console.WriteLine("{0}:", Type);
            Console.WriteLine("\tArea: {0:N2}", Area());
            Console.WriteLine("\tPerimeter: {0:N2}", Perimeter());
        }
    }

    class Rectangle : Figure
    {
        private double width;
        private double height;

        public Rectangle()
        {
            width = height = 0;
        }

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public override string Type
        {
            get { return "Rectangle (" + width.ToString() + "x" + height.ToString() + ")"; }
        }

        public override double Area()
        {
            return width * height;
        }

        public override double Perimeter()
        {
            return 2 * (width + height);
        }
    }

    class Circle : Figure
    {
        private double radius;

        public Circle()
        {
            radius = 0;
        }

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public override string Type
        {
            get { return "Circle (R = " + radius.ToString() + ")"; }
        }

        public override double Area()
        {
            return Math.PI * radius * radius;
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    class Triangle : Figure
    {
        private double a;
        private double b;
        private double c;

        public Triangle()
        {
            a = b = c = 0;
        }

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double A
        {
            get { return a; }
            set { a = value; }
        }

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        public double C
        {
            get { return c; }
            set { c = value; }
        }

        public override string Type
        {
            get { return "Triangle"; }
        }

        public override double Area()
        {
            double p = Perimeter() / 2;

            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public override double Perimeter()
        {
            return a + b + c;
        }
    }
}