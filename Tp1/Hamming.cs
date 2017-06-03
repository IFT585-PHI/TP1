using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author : Iannick Langevin

namespace Tp1
{
    public class Hamming
    {
        public static String Encode(String data)
        {
            int nbBitCorrector = 0;
            int posBitCorrector = 0;
            List<char> byteList = StringToBinary(data);
            List<int> indexList = new List<int>();

            //Add all the bits corrector initialized at 0
            do
            {
                indexList.Add(posBitCorrector);
                byteList.Insert(posBitCorrector, '0');
                nbBitCorrector++;
                posBitCorrector = (int)(Math.Pow(2, nbBitCorrector)) - 1;

            } while (posBitCorrector < byteList.Count);

            //Put the respective value for each bits corrector
            foreach (int i in indexList)
            {
                byteList[i] = CalculateBitCorrectorForPos(byteList, i + 1);
            }

            return string.Join(String.Empty, byteList.ToArray());
        }

        public static String Decode(String data)
        {
            List<char> byteList = new List<char>();

            // Shitty name, mea culpa
            // Key = posBitCorrector, Value : 0 = no error, 1 = error
            Dictionary<int, int> indexError = new Dictionary<int, int>();

            foreach (char c in data)
            {
                byteList.Add(c);
            }

            // Look if there is a bit corrector error and fix it
            if (HasBitCorrectorError(byteList, ref indexError))
            {
                foreach(KeyValuePair<int, int> v in indexError)
                {
                    if (v.Value == 0)
                        byteList[v.Key] = ((byteList[v.Key] == '0') ? '1' : '0');
                }
            }

            // Remove the bit corrector from the binary string.
            // Need to reverse the dictionnary because indexes die when you remove them from the beginning of a string... #so much time lost here
            foreach (KeyValuePair<int, int> v in indexError.Reverse())
            {
                byteList.RemoveAt(v.Key);
            }

            return BinaryToString(string.Join(String.Empty, byteList.ToArray()));
        }

        private static Boolean Validate()
        {
            return true;
        }

        private static List<char> StringToBinary(String data)
        {
            List<char> byteList = new List<char>();

            Byte[] bytes = Encoding.ASCII.GetBytes(data);
            String binaryString = string.Join(String.Empty, bytes.Select(b => Convert.ToString(b, 2).PadLeft(7, '0')));

            foreach (char c in binaryString)
                byteList.Add(c);

            return byteList;
        }

        private static String BinaryToString(String data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 7)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 7), 2));
            }

            return  Encoding.ASCII.GetString(byteList.ToArray());
        }

        /*
         * https://en.wikipedia.org/wiki/Hamming_code
         *      0   1   2   3   4   5   6   7   8   9   10  11
         * P1   X       X       X       X       X       X
         * P2       X   X           X   X           X   X
         * P3               X   X   X   X                   X
         * P4                               X   X   X   X   X
         */
        private static char CalculateBitCorrectorForPos(List<char> byteList, int noBits)
        {
            int somme = 0;
            int index = noBits - 1;

            while (index < byteList.Count)
            {
                // Calculate adjacents bits
                for(int i = 0; i < noBits && index + i < byteList.Count; i++)
                {
                    // Don't want to add the corrector bit value when we decode...
                    if(index != noBits - 1)
                    {
                        somme += (int)Char.GetNumericValue(byteList[index + i]);
                    }
                }

                // Jump to the next segment
                index += 2 * noBits;
            }

            return (somme % 2).ToString()[0];
        }

        public static Boolean HasBitCorrectorError(List<char> byteList, ref Dictionary<int, int> indexError)
        {
            int nbBitCorrector = 0;
            int posBitCorrector = 0;

            do
            {
                // Calculate the bit corrector and validate it against our current data
                indexError[posBitCorrector] = ((CalculateBitCorrectorForPos(byteList, posBitCorrector + 1) == byteList[posBitCorrector]) ? 0 : 1);
                nbBitCorrector++;
                posBitCorrector = (int)(Math.Pow(2, nbBitCorrector)) - 1;
            } while (posBitCorrector < byteList.Count);

            foreach(KeyValuePair<int, int> v in indexError)
            {
                if(v.Value == 1)
                    return true;
            }

            return false;
        }
    }
}
