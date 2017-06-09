using System;
using System.IO;
namespace Tp1
{
    class Transmitter
    {
        public void Transmitting(Inputs input)
        {
            int frameIndex = 0;
            int bufferFreeIndex = 0;
            bool transBufferIsFull = false;
            Frame[] TransmitterBuffer = new Frame[input.BufferSize];

            string fileContent = File.ReadAllText(@input.SourceFileName);
            string encodedContent = "Bob";//= Encoder(fileContent);

            int frameCount = encodedContent.Length / 1024;
            
            if (encodedContent.Length % 1024 != 0)
                frameCount += 1;

            while (frameIndex < frameCount)
            {
                if(!transBufferIsFull)
                {
                    Frame frame = new Frame();
                    frame.FrameId = frameIndex;
                    frame.Message = fileContent.Substring(frameIndex * 1024, 1024).ToCharArray();
                    //frame.Code = ????

                    TransmitterBuffer[frameIndex % input.BufferSize] = frame;
                }

                //Envoit + timer

                if(true)//Code de la trame recu == ACk
                {
                    Frame receivedFrame = new Frame();



                }
                else if (false) //Code de la trame recu 
                {

                }
                


            }
            

        }
    }
}
