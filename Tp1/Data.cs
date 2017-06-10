using System;

namespace Tp1
{
    /// <summary>
    /// Class that represents the reciever on a frame.
    /// </summary>
    public class Frame
    {
        public char[] Message = new char[8];
        public int FrameId { get; set; }
        public Type type;
    }

    public enum Type
    {
        Data, 
        Nak, //Error code
        Ack, //Success code
        Fin
    }

    struct Inputs
    {
        const string INPUTS_FILENAME = "../../inputs.txt";
        public int BufferSize;
        public long Delay;
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
                Console.WriteLine("Source FileName: " + SourceFileName);
                Console.WriteLine("Destination FileName: " + DestinationFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
