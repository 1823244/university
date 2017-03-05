using System;
using System.IO;
using Lab4;

namespace Lab5
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("Файл с алфавитом [ab.txt]: ");
            string path = Console.ReadLine();
            if (path == "")
            {
                path = "ab.txt";
            }
            StreamReader streamReader = new StreamReader(path);
            path = "";
            ArithmeticCoding AC = new ArithmeticCoding(streamReader);
            streamReader.Close();

            Console.Write("Файл с сообщением [msg.txt]: ");
            path = Console.ReadLine();
            if (path == "")
            {
                path = "msg.txt";
            }
            streamReader = new StreamReader(path);
            path = "";
            string message = streamReader.ReadLine();
            streamReader.Close();

            Console.Write("Файл вывода для кода [code.txt]: ");
            path = Console.ReadLine();
            if (path == "")
            {
                path = "code.txt";
            }
            StreamWriter streamWriter = new StreamWriter(path);
            AC.EncodingMessage(message, streamWriter);
            streamWriter.Close();
            
            streamReader = new StreamReader(path);
            path = "";
            Console.Write("Файл вывода для сообщения [out.txt]: ");
            path = Console.ReadLine();
            if (path == "")
            {
                path = "out.txt";
            }
            streamWriter = new StreamWriter(path);
            AC.DecodingMessage(streamReader, streamWriter);
            streamWriter.Close();
            streamReader.Close();
        }
    }
}
