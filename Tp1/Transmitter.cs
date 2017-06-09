using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    class Transmitter
    {
        InterThreadSynchronizer synchronizer;
        Dictionary<int, Stopwatch> framesTimer;

        public Transmitter(InterThreadSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer;
        }


        public void Transmitting(Inputs input, EncodingAlgo algo)
        {
            int frameIndex = 0;
            int bufferUsedCellCount = 0;
            Frame[] TransmitterBuffer = new Frame[input.BufferSize];

            string fileContent = File.ReadAllText(@input.SourceFileName);

            int frameCount = fileContent.Length / 1014;
            
            if (fileContent.Length % 1024 != 0)
                frameCount += 1;

            while (frameIndex < frameCount)
            {
                if(bufferUsedCellCount < input.BufferSize)
                {
                    Frame frame = new Frame();
                    frame.FrameId = frameIndex;

                    string encodedMessage = Hamming.Encode(fileContent.Substring(frameIndex * 1014, 1014));
                    
                    frame.Message = encodedMessage.Substring(1024, 1024).ToCharArray();
                    frame.Code = algo;

                    TransmitterBuffer[frameIndex % input.BufferSize] = frame;

                    //Envoit

                    Stopwatch frameTimer = new Stopwatch();
                    frameTimer.Start();
                    framesTimer[frameIndex] = frameTimer;

                    bufferUsedCellCount++;
                    frameIndex++;
                }

                Frame receivedFrame = synchronizer.GetMessageFromSource();

                if (receivedFrame.successCode == SuccessCode.Ack)//Code de la trame recu == ACk
                {
                    //enlever la trame du buffer
                    TransmitterBuffer[receivedFrame.FrameId % input.BufferSize] = null;
                    bufferUsedCellCount--;
                    //Arret du timer

                }
                else if (receivedFrame.successCode == SuccessCode.Nack) //Code de la trame recu
                {
                    //Renvoyer la trame en erreur
                }

                foreach (var timer in framesTimer)
                {
                    if(timer.Value.ElapsedMilliseconds >= input.Delay)
                    {
                        //Renvoyer la trame et repartir le timer pour la trame

                        timer.Value.Reset();
                        timer.Value.Start();
                    }
                }
            }
        }
    }
}
