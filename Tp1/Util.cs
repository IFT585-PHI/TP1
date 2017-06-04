using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author: Iannick Langevin

namespace Tp1
{
    public class Util
    {
        ///<summary>
        ///Convert a string of char into a string of binary
        ///</summary>
        public static List<char> CharToBinary(string data)
        {
            List<char> result = new List<char>();

            Byte[] bytes = Encoding.ASCII.GetBytes(data);
            String binaryString = string.Join(string.Empty, bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
            result.AddRange(binaryString);

            return result;
        }

        ///<summary>
        ///Convert a string of binary into a string of char
        ///</summary>
        public static string BinaryToChar(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        ///<summary>
        ///Convert a list of char into a binary string
        ///</summary>
        public static string ListToBinary(List<char> list)
        {
            return string.Join(string.Empty, list.ToArray());
        }

        public static void InjectErrorAtPosition(ref string codedData, int pos)
        {
            StringBuilder s = new StringBuilder(codedData);
            s[pos] = ((s[pos] == '0') ? '1' : '0'); ;
            codedData = s.ToString();
        }

        public static void InjectErrorRandom(ref string codedData)
        {
            Random r = new Random();
            int ra = r.Next(0, codedData.Length - 1);
            InjectErrorAtPosition(ref codedData, ra);

            Console.WriteLine(ra);
            System.Threading.Thread.Sleep(100);
        }
    }
}
