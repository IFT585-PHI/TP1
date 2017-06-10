using System;
using System.Collections.Generic;
using System.Linq;

//Author : Iannick Langevin

namespace Tp1
{
    public class Hamming
    {
        /// <param name="data">string of char</param>
        /// <returns>binary string</returns> 
        public static string Encode(string data)
        {
            List<char> byteList = new List<char>(data);
            List<int> indexList = FindAllIndexBitCorrector(byteList);

            //Insert all bit correctors
            foreach(int i in indexList)
            {
                byteList.Insert(i, '0');
            }
            //Insert the last bit corrector for 2 errors detection
            byteList.Add('0');

            //Put the respective value for each bits corrector
            foreach (int i in indexList)
            {
                byteList[i] = CalculateBitCorrectorForPos(byteList, i + 1);
            }
            byteList[byteList.Count-1] = CalculateLastBitCorrector(byteList);

            return Util.ListToBinary(byteList);
        }

        /// <param name="data">binary string</param>
        /// <returns>string of char</returns> 
        public static string Decode(char[] codedData)
        {
            List<char> byteList = new List<char>(codedData);
            List<int> indexBitCorrector = FindAllIndexBitCorrector(byteList);
            indexBitCorrector.Add(byteList.Count - 1);

            // Remove the bit corrector from the binary string, starting from the end
            indexBitCorrector.Reverse();
            foreach (int i in indexBitCorrector)
            {
                byteList.RemoveAt(i);
            }

            return Util.BinaryToChar(Util.ListToBinary(byteList));
        }

        /// <param name="data">binary string</param>
        public static Boolean Validate(ref char[] codedData)
        {
            List<char> byteList = new List<char>(codedData);

            Char newLastBitValue = CalculateLastBitCorrector(byteList);
            Char oldLastBitValue = byteList.Last();

            //Remove it because it was initialized at 0 and may affect other bit corrector
            byteList.RemoveAt(byteList.Count - 1);

            List<int> indexBitCorrector = FindAllIndexBitCorrector(byteList);
            List<int> indexBitCorrectorError = FindBitCorrectorError(byteList, indexBitCorrector);
            int bitCorrectorErrorCount = indexBitCorrectorError.Count;

            // If there is 0 bit error detected, it means there is no error UNLESS our last bit corrector flipped
            if (bitCorrectorErrorCount == 0 && oldLastBitValue == newLastBitValue)
            {
                return true;
            }
            // If there is 1 bit error detected, it means there is one error UNLESS our last bit corrector didnt flipped
            else if (bitCorrectorErrorCount == 1 && oldLastBitValue != newLastBitValue)
            {
                // Then the error is the bit corrector itself
                int indexBadBit = indexBitCorrectorError[0];
                byteList[indexBadBit] = ((byteList[indexBadBit] == '0') ? '1' : '0');

                for (int i = 0; i < byteList.Count; i++)
                    codedData[i] = byteList[i];

                return true;
            }

            // If there is more than 2 bits corrector error, add them to get your faulty index
            if (bitCorrectorErrorCount >= 2)
            {
                int indexBadBit = indexBitCorrectorError.Sum() + indexBitCorrectorError.Count() - 1;
                
                // This means we have at least 2 errors
                if (indexBadBit > byteList.Count - 1)
                {
                    return false;
                }             
                byteList[indexBadBit] = ((byteList[indexBadBit] == '0') ? '1' : '0');

                //Add the last bit corrector to get the new last bit corrector value
                byteList.Add(oldLastBitValue);
                newLastBitValue = CalculateLastBitCorrector(byteList);
                byteList.RemoveAt(byteList.Count - 1);

                indexBitCorrectorError = FindBitCorrectorError(byteList, indexBitCorrector);

                // If the last bit corrector didn't flip and there is no more error,
                // we have correctly fixed our error
                if (oldLastBitValue == newLastBitValue && indexBitCorrectorError.Count == 0)
                {
                    for (int i = 0; i < byteList.Count; i++)
                        codedData[i] = byteList[i];

                    return true;
                }
            }  

            return false;
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
         *      1   2   3   4   5   6   7   8   9   10  11 12 LA
         * P1   X       X       X       X       X       X
         * P2       X   X           X   X           X   X
         * P3               X   X   X   X                   X
         * P4                               X   X   X   X   X
         * LA   X   X   X   X   X   X   X   X   X   X   X   X
         */
        private static char CalculateBitCorrectorForPos(List<char> byteList, int noBits)
        {
            int somme = 0;
            int index = noBits - 1;

            while (index < byteList.Count)
            {        
                // Calculate adjacents bits                              
                for (int i = 0; i < noBits && index + i < byteList.Count; i++)
                {
                    // Don't want to add the corrector bit value when decoded because they were all initialized at 0
                    if(index + i != noBits - 1)
                    {
                        somme += (int)Char.GetNumericValue(byteList[index + i]);
                    }
                        
                }       

                // Jump to the next segment
                index += 2 * noBits;
            }

            return (somme % 2).ToString()[0];
        }

        private static char CalculateLastBitCorrector(List<char> byteList)
        {
            int somme = 0;

            // Calculate all bits
            for (int i = 0; i < byteList.Count - 1; i++)
            {
                somme += (int)Char.GetNumericValue(byteList[i]);
            }

            return (somme % 2).ToString()[0];
        }

        private static List<int> FindBitCorrectorError(List<char> byteList, List<int> indexBitCorrector)
        {
            List<int> result = new List<int>();

            foreach (int noBits in indexBitCorrector)
            {
                char a = CalculateBitCorrectorForPos(byteList, noBits + 1);
                if (byteList[noBits] != CalculateBitCorrectorForPos(byteList, noBits + 1))
                {
                    result.Add(noBits);
                }
            }

            return result;
        }
    }
}
