using System;

namespace Lab6
{
    class MainClass
    {
        public static double func(double x, double y)
        {
            return x * x + 2 * y * y + 2 * (x + y) + 5;
        }

        public static double gradFuncX(double x, double y)
        {
            return 2 * x + 2;
        }

        public static double gradFuncY(double x, double y)
        {
            return 4 * y + 2;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("{0}", MultidimensionalMinimization.GradientMethodWithFractionalSteps(
                func,
                gradFuncX, gradFuncY,
                2, 1,  // приближение
                0.0001,
                50000,
                0.5, 0.7, 0.6 // параметры метода 
                )
            );
            Console.WriteLine("\nPress any key..");
            Console.ReadLine();
        }
    }
}
