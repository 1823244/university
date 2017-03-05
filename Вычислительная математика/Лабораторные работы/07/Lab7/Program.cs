using System;

namespace Lab7
{
    class MainClass
    {
        public static double F(double x, double y)
        {
            return Math.Sin(x + 2) - y - 1.5;
            //return Math.Sin(x + y) - 1.2 * x - 0.1;
        }

        public static double dFx(double x, double y)
        {
            return Math.Cos(x + 2);
            //return Math.Cos(x + y) - 1.2;
        }

        public static double dFy(double x, double y)
        {
            return -1;
            //return Math.Cos(x + y);
        }

        public static double G(double x, double y)
        {
            return x + Math.Cos(y - 2) - 0.5;
            //return x * x + y * y - 1;
        }

        public static double dGx(double x, double y)
        {
            return 1;
            //return 2 * x;
        }

        public static double dGy(double x, double y)
        {
            return -Math.Sin(y - 2);
            //return 2 * y;
        }

        public static void Main(string[] args)
        {
            double x = 2;
            double y = 1;

            NewtonMethod.NewtonMethodFunc(
                F, dFx, dFy,
                G, dGx, dGy,
                ref x, ref y,
                0.01,
                200
            );

            Console.WriteLine("x = {0}\t y = {1}", x, y);
            Console.ReadLine();
        }
    }
}
