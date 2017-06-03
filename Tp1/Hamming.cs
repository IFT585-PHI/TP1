using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    public struct Result
    {
        public String data;
        public Boolean isValid;
    }

    public class Hamming
    {
        public static void Encode(String d)
        {
            int nbBitCorrector = 0;
            int posBitCorrector = 0;

            String data = "a";
            List<char> byteList = ToBinaryString(data);
            List<int> indexList = new List<int>();

            //Add all the bits corrector initialized at 0
            do
            {
                indexList.Add(posBitCorrector);
                byteList.Insert(posBitCorrector, '0');
                nbBitCorrector++;
                posBitCorrector = (int)(Math.Pow(2, nbBitCorrector)) - 1;

            } while (posBitCorrector < byteList.Count);

            //Puts the respective value for each bits corrector
            foreach (int i in indexList)
            {
                byteList[i] = CalculateBitCorrectorForPos(byteList, i + 1);
            }

            foreach (char c in byteList)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            Console.ReadLine();
        }

        public static Result Decode()
        {
            Result r;
            r.data = "a";
            r.isValid = Validate();

            return r;
        }

        private static Boolean Validate()
        {
            return true;
        }

        private static List<char> ToBinaryString(String a)
        {
            List<char> byteList = new List<char>();

            //ASCII Table goes from 0 to 127, so 7 binaries are sufficient to hold the char value
            Byte[] b = Encoding.ASCII.GetBytes(a);
            String binaryString = string.Join("", b.Select(byt => Convert.ToString(byt, 2).PadLeft(7, '0')));

            foreach (char c in binaryString)
                byteList.Add(c);

            return byteList;
        }

        private static char CalculateBitCorrectorForPos(List<char> byteList, int noBits)
        {
            int somme = 0;
            int index = noBits - 1;

            while (index < byteList.Count)
            {
                for(int i = 0; i < noBits && index + i < byteList.Count; i++)
                {
                    somme += (int)Char.GetNumericValue(byteList[index + i]);
                }

                index += 2 * noBits;
            }

            return (somme % 2).ToString()[0];
        }
    }
}
