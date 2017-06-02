using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    class Program
    {

        const string INPUTS_FILENAME = "../../inputs.txt";
        static int BufferSize;
        static int Delay;
        static string SourceFileName;
        static string DestinationFileName;


        static void Main(string[] args)
        {
            ReadInputs();


        }

        static void ReadInputs()
        {
            try
            {
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
