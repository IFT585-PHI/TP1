using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    static class Logger
    {
        public static string ReadString(string message)
        {
            Console.Write(message + " ");
            return Console.ReadLine();
        }
        public static bool ReadStringChoice(string message)
        {
            Console.Write(message + " (y/n) ");
            string r = Console.ReadLine();

            while (r != "y" && r != "n")
                r = Console.ReadLine();

            return r == "y";
        }
        public static int ReadInt(string message)
        {
            Console.Write(message + " ");
            return ReadParseInt();
        }
        public static int ReadIntInterval(string message, int min, int max)
        {
            Console.Write(message + " [" + min + "-" + max + "] ");
            int pos = ReadParseInt();

            while (pos < min && pos > max)
                pos = ReadParseInt();

            return pos;
        }

        private static int ReadParseInt()
        {
            string line = Console.ReadLine();
            int value;
            while (!int.TryParse(line, out value))
            {
                line = Console.ReadLine();
            }
            return value;
        }
    }
}
