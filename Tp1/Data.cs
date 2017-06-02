using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    public class Weft
    {
        /* 
         *  
         */
        public const int WEFT_SIZE = 7;
        string Message { get; set; }
        int WeftId { get; set; }
        int DetectorCode { get; set; }
        int CorrectorCode { get; set; }
    }

    struct Inputs
    {
        const string INPUTS_FILENAME = "../../inputs.txt";
        public int BufferSize;
        public int Delay;
        public string SourceFileName;
        public string DestinationFileName;

        public void ReadInputs()
        {
            try
            {
                //Read the inputs for the program
                System.IO.StreamReader file = new System.IO.StreamReader(INPUTS_FILENAME);
                BufferSize = int.Parse(file.ReadLine());
                Delay = int.Parse(file.ReadLine());
                SourceFileName = file.ReadLine();
                DestinationFileName = file.ReadLine();
                Console.WriteLine("BufferSize: " + BufferSize);
                Console.WriteLine("Delay: " + Delay);
                Console.WriteLine("SourceFileName: " + SourceFileName);
                Console.WriteLine("DestinationFileName: " + DestinationFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
