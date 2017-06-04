using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author : Iannick Langevin

namespace Tp1
{
    public class Hamming
    {
        /// <param name="data">string of char</param>
        /// <returns>binary string</returns> 
        public static string Encode(string data)
        {
            List<char> byteList = Util.CharToBinary(data);
            List<int> indexList = FindAllIndexBitCorrector(byteList);

            //Insert all bit correctors
            foreach(int i in indexList)
            {
                byteList.Insert(i, '0');
            }

            //Put the respective value for each bits corrector
            foreach (int i in indexList)
            {
                byteList[i] = CalculateBitCorrectorForPos(byteList, i + 1);
            }

            return Util.ListToBinary(byteList);
        }

        /// <param name="data">binary string</param>
        /// <returns>string of char</returns> 
        public static string Decode(string codedData)
        {
            List<char> byteList = new List<char>();
            byteList.AddRange(codedData);
            List<int> indexBitCorrector = FindAllIndexBitCorrector(byteList);

            // Remove the bit corrector from the binary string, starting from the end
            indexBitCorrector.Reverse();
            foreach (int i in indexBitCorrector)
            {
                byteList.RemoveAt(i);
            }

            return Util.BinaryToChar(Util.ListToBinary(byteList));
        }

        /// <param name="data">binary string</param>
        public static Boolean Validate(string codedData)
        {
            List<char> byteList = new List<char>();
            byteList.AddRange(codedData);
            List<int> indexBitCorrector = FindAllIndexBitCorrector(byteList);
            List<int> indexBitCorrectorError = FindBitCorrectorError(byteList, indexBitCorrector);

            // First validation
            // If there is only one bit corrector error, the bit itself is the error
            if (indexBitCorrectorError.Count == 1)
            {
                int indexBadBit = indexBitCorrectorError[0];
                byteList[indexBadBit] = ((byteList[indexBadBit] == '0') ? '1' : '0');
            }
            // If there is more than 2 bits corrector error, add them to get your faulty index
            else if (indexBitCorrectorError.Count >= 2)
            {
                int indexBadBit = indexBitCorrectorError.Sum() + 1;
                if(indexBadBit > byteList.Count)
                {
                    return false;
                }
                byteList[indexBadBit] = ((byteList[indexBadBit] == '0') ? '1' : '0');
            }

            //2e vérif
            indexBitCorrector = FindAllIndexBitCorrector(byteList);
            indexBitCorrectorError = FindBitCorrectorError(byteList, indexBitCorrector);
            // If there is a second error, we can't do anything about it
            if(indexBitCorrectorError.Count != 0)
            {
                return false;
            }

            return true;
        }

        private static List<int> FindAllIndexBitCorrector(List<char> byteList)
        {
            List<int> result = new List<int>();

            int nbBitCorrector = 0;
            int posBitCorrector = 0;
            do
            {
                result.Add(posBitCorrector);
                nbBitCorrector++;
                posBitCorrector = (int)(Math.Pow(2, nbBitCorrector)) - 1;

            } while (posBitCorrector <= byteList.Count);

            return result;
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

           // Console.WriteLine(noBits);

            while (index < byteList.Count)
            {        
                // Calculate adjacents bits
                for (int i = 0; i < noBits && index + i < byteList.Count; i++)
                {
                    // Don't want to add the corrector bit value when decoded because they were all initialized at 0
                    if(index + i != noBits - 1)
                    {
                        //Console.Write(index + i + " ");
                        somme += (int)Char.GetNumericValue(byteList[index + i]);
                    }
                }

                // Jump to the next segment
                index += 2 * noBits;
            }

            //Console.WriteLine();

            char a = (somme % 2).ToString()[0];
            return (somme % 2).ToString()[0];
        }

        private static List<int> FindBitCorrectorError(List<char> byteList, List<int> indexBitCorrector)
        {
            List<int> result = new List<int>();

            foreach (int noBits in indexBitCorrector)
            {
                if (byteList[noBits] != CalculateBitCorrectorForPos(byteList, noBits + 1))
                {
                    result.Add(noBits);
                }
            }

            return result;
        }
    }
}
