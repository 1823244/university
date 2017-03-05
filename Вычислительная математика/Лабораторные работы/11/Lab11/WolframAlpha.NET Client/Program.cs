using System;
using System.Configuration;
using Lab11Namespace;
using Lab11Namespace.Misc;
using Lab11Namespace.Objects;
using System.Threading;

namespace WolframAlphaNETClient
{
    public class Program
    {
        private static string _allId = ConfigurationManager.AppSettings["AppId"];

        static double Func(double x)
        {
            return 2 * Math.Sin(x) + x;
        }

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Console.Write("Input a, b and n: ");
            string[] input = Console.ReadLine().Split(' ');
            double a = Double.Parse(input[0]);
            double b = Double.Parse(input[1]);
            int n = Int32.Parse(input[2]);
            double h = (b - a) / n;
            string query = "";

            Console.WriteLine("{0,7}{1,7}{2,10}", "i", "x", "y");
            for (int i = 0; i <= n; i++)
            {
                double f = Func(a);
                Console.WriteLine("{0,7}{1,7}{2,10:N5}", i, a, f);
                a += h;
                query += "{" + String.Format("{0:N5}", a.ToString()) + "," + String.Format("{0:N5}", f.ToString()) + "},";
            }
            Console.WriteLine();
            Console.ReadLine();

            Core core = new Core(_allId);
            //core.ScanTimeout = 3.0f;
            core.UseTLS = false;

            query = query.Substring(0, query.Length - 1);
            QueryResult results = core.Query(String.Format("linear fit {0}", query));
            Console.Write("F1 = ");
            GetResult(results);

            QueryResult results2 = core.Query(String.Format("quadratic fit {0}", query));
            Console.Write("F2 = ");
            GetResult(results2);

            QueryResult results3 = core.Query(String.Format("cubic fit {0}", query));
            Console.Write("F3 = ");
            GetResult(results3);

            Console.WriteLine("Press any key..");
            Console.ReadLine();
        }

        static void GetResult(QueryResult res)
        {
            res.RecalculateResults();
            Pod neededPod = null;

            if (res.Error != null)
            {
                GetResult(res);
                return;
            }

            if (res.Pods != null)
            {
                foreach (Pod item in res.Pods)
                {
                    if (item.ID == "LeastSquaresBestFit")
                    {
                        neededPod = item;
                    }
                }
            }
            else
            {
                GetResult(res);
                return;
            }

            if (neededPod != null)
            {
                if (neededPod.SubPods.HasElements())
                {
                    string s = neededPod.SubPods[0].Plaintext.Replace("x", "* x").Replace("-", " - ").Replace("+", " + ");
                    if (s.IndexOf('(') > -1)
                    {
                        s = s.Split('(')[0];
                    }
                    if (s[0] == ' ' && s[1] == '-' && s[2] == ' ')
                    {
                        s = s.Substring(3, s.Length - 3);
                        s = "-" + s;
                    }
                    Console.WriteLine(s);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Whoops..");
            }
        }
    }
}