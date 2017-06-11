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
        ///Convert a string of 8 binary into a char
        ///</summary>
        public static char BinaryToChar(string data)
        {
            List<Byte> byteList = new List<Byte>();

            if(data.Length % 8 != 0)
            {
                data = data.TrimStart('0');

                while (data.Length % 8 != 0)
                {
                    data = "0" + data;
                }
            }

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            char a = Encoding.ASCII.GetString(byteList.ToArray())[0];
            return a;
        }

        ///<summary>
        ///Convert a list of char into a string
        ///</summary>
        public static string ListToString(List<char> list)
        {
            return string.Join(string.Empty, list.ToArray());
        }

        public static void InjectErrorAtPosition(ref char[] codedData, int pos)
        {
            codedData[pos] = ((codedData[pos] == '0') ? '1' : '0'); ;
        }

        public static void InjectErrorRandom(ref char[] codedData)
        {
            Random r = new Random();
            int ra = r.Next(0, codedData.Length - 1);
            InjectErrorAtPosition(ref codedData, ra);

            System.Threading.Thread.Sleep(100);
        }
    }
}
