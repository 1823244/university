using System;

namespace Lab5
{
	class MainClass
	{
		//Наша функция
		public static double func(double x) {
			return 2*x-Math.Pow (Math.Pow(x, 2), 1 / 3.0);
		}

		public static void Main (string[] args)
		{
			//Метод оптимального поиска
			Console.WriteLine ("Метод оптимального поиска на отрезке [{0};{1}] с шагом {2}: \n\t{3}",
				0, 0.1, 0.00001,
				DimensionalMinimization.MethodOfOptimalSearch(0, 0.1, func, 0.00001));
			
            //Метод деления отрезка пополам
			Console.WriteLine ("Метод деления отрезка пополам на отрезке [{0};{1}] с точностью {2}: \n\t{3}",
				0, 0.1, 0.0001,
				DimensionalMinimization.MethodOfBisectionOfTheInterval(0, 0.1, func, 0.0001));
			
            //Метод основанный на числах Фибоначчи
			Console.WriteLine ("Метод основанный на числах Фибоначчи на отрезке [{0};{1}] с точностью {2}: \n\t{3}",
				0, 0.1, 0.0001,
				DimensionalMinimization.FibonacciMethod(0, 0.1, func, 0.0001));
			
            //Метод «Золотого сечения»
			Console.WriteLine ("Метод «Золотого сечения» на отрезке [{0};{1}] с точностью {2}: \n\t{3}",
				0, 0.1, 0.0001,
				DimensionalMinimization.GoldenSectionMethod(0, 0.1, func, 0.0001));

			Console.WriteLine ("\nPress any key..");
            Console.ReadLine();
		}
	}
}
