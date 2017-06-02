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

        struct Inputs{
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


        static void Main(string[] args)
        {
            Inputs inputs = new Inputs();
            inputs.ReadInputs();
            
            

        }

        
    }
}
