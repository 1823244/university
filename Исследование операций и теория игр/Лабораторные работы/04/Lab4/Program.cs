using System;
using System.IO;

namespace Lab4
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			StreamReader streamReader = new StreamReader("input.txt");
			StreamWriter streamWriter = new StreamWriter("output.txt");

			TransportationTheory tr = new TransportationTheory (streamReader);
			tr.PrintMatr (streamWriter);
			tr.DistributionMethod (streamWriter);

			streamReader.Close();
			streamWriter.Close();
		}
	}
}
