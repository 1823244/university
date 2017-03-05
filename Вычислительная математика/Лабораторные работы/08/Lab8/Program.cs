using System;

namespace Lab8
{
	class MainClass
	{
		public static double F (double x)
		{
            //return 2.2 * x - Math.Pow(2, x);
            return x * x * x + 3 * x * x + 12 * x + 3;
		}

		public static double dFdx (double x)
		{
            //return 2.2 - Math.Log(2) * Math.Pow(2, x);
            return 3 * (x * x + 2 * x + 4);
		}

		public static double ddFdx (double x)
		{
            //return -Math.Pow(2, x) * Math.Pow(Math.Log(2), 2);
            return 6 * (x + 1);
		}

		public static void Main (string[] args)
		{
			Console.WriteLine ("x = {0}",
				CombinedMethod.CombinedMethodFunc (
					F,
					dFdx,
					ddFdx,
                    2, 3,
                    //-1, 0,
					0.001)
			);
            Console.ReadLine();
		}
	}
}
