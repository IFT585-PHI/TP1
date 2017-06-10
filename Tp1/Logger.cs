using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    static class Logger
    {
        ///<Summary> 
        ///Display message.
        ///</Summary>
        ///<returns> </returns>
        public static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
        ///<Summary> 
        ///Display message.
        ///</Summary>
        ///<returns> </returns>
        public static void WriteMessage(char[] message)
        {
            Console.Write(message);
        }
        ///<Summary> 
        ///Ask the user for a string value.
        ///</Summary>
        ///<returns> What the user entered in the console.</returns>
        public static string ReadString(string message)
        {
            Console.Write(message + " ");
            return Console.ReadLine();
        }
        ///<Summary> 
        ///Ask a yes/no question to the user
        ///</Summary>
        ///<returns> Boolean depending if the user want or not the message to be true (y/n).</returns>
        public static bool ReadStringChoice(string message)
        {
            Console.Write(message + " (y/n) ");
            string r = Console.ReadLine();

            while (r != "y" && r != "n")
                r = Console.ReadLine();

            return r == "y";
        }
        ///<Summary> 
        ///Ask the user for a int value.
        ///</Summary>
        ///<returns> Int value.</returns>
        public static int ReadInt(string message)
        {
            Console.Write(message + " ");
            return ReadParseInt();
        }
        ///<Summary> 
        ///Ask the user for a int value between the min and max.
        ///</Summary>
        ///<returns> Int value.</returns>
        public static int ReadIntInterval(string message, int min, int max)
        {
            Console.Write(message + " [" + min + "-" + max + "] ");
            int pos = ReadParseInt();

            while (pos < min && pos > max)
                pos = ReadParseInt();

            return pos;
        }
        ///<Summary> 
        ///Ask the user for a int, while it's not a int.
        ///</Summary>
        ///<returns> Int value.</returns>
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
