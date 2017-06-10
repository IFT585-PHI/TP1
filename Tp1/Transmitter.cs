using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Tp1
{
    class Transmitter
    {
        InterThreadSynchronizer synchronizer;
        Dictionary<int, Stopwatch> framesTimer;

        public Transmitter(InterThreadSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer;
            framesTimer = new Dictionary<int, Stopwatch>();
        }

        public void Transmitting(Inputs input)
        {
            Console.WriteLine("\n Transmitting start \n");
            int frameIndex = 0;
            int bufferUsedCellCount = 0;
            bool lastFrameReceived = false;
            Frame[] TransmitterBuffer = new Frame[input.BufferSize];

            string fileContent = File.ReadAllText(@input.SourceFileName);

            fileContent = Util.ListToBinary(Util.CharToBinary(fileContent));

            int frameCount = fileContent.Length / 8;
            
            if (fileContent.Length % 8 != 0)
                frameCount += 1;

            fileContent = fileContent.PadLeft(8 * frameCount, '0');

            while (frameIndex <= frameCount && !lastFrameReceived)
            {
                if (bufferUsedCellCount < input.BufferSize && frameIndex <= frameCount)
                {
                    Frame frame = new Frame();
                    frame.FrameId = frameIndex;

                    string encodedMessage = string.Empty;

                    int i = fileContent.Length;
                    if (frameIndex < frameCount)
                    {
                        encodedMessage = Hamming.Encode(fileContent.Substring(frameIndex * 8, 8));
                        frame.Message = encodedMessage.Substring(0, 13).ToCharArray();
                    }

                    int j = encodedMessage.Length;

                    frame.type = (frameIndex < frameCount) ? Type.Data : Type.Fin;

                    TransmitterBuffer[frameIndex % input.BufferSize] = frame;
                    //Console.WriteLine(Util.BinaryToHexa(frame.Message));
                    while (!synchronizer.TransferTrameToSupportSource(frame)) ;

                    Stopwatch frameTimer = new Stopwatch();
                    framesTimer[frameIndex] = frameTimer;
                    framesTimer[frameIndex].Start();

                    bufferUsedCellCount++;
                    frameIndex++;
                }

                while (!synchronizer.ReadyToReadDestinationMessage()) ;

                Frame receivedFrame = synchronizer.GetMessageFromDestination();

                if (receivedFrame != null)
                { 
                    if (receivedFrame.type == Type.Fin)
                    {
                        lastFrameReceived = true;
                    }
                    else if (receivedFrame.type == Type.Ack)//Code de la trame recu == ACk
                    {
                        //enlever la trame du buffer
                        TransmitterBuffer[receivedFrame.FrameId % input.BufferSize] = null;
                        bufferUsedCellCount--;
                        framesTimer[receivedFrame.FrameId].Stop();
                        framesTimer.Remove(receivedFrame.FrameId);
                    }
                    else if (receivedFrame.type == Type.Nak) //Code de la trame recu
                    {
                        //Renvoyer la trame en erreur
                        while (synchronizer.TransferTrameToSupportSource(TransmitterBuffer[receivedFrame.FrameId % input.BufferSize])) ;

                        framesTimer[receivedFrame.FrameId].Reset();
                        framesTimer[receivedFrame.FrameId].Start();
                    }
                }
                foreach (var timer in framesTimer)
                {
                    if(timer.Value.ElapsedMilliseconds >= input.Delay)
                    {
                        //Renvoyer la trame
                        while (synchronizer.TransferTrameToSupportSource(TransmitterBuffer[timer.Key % input.BufferSize])) ;
                        timer.Value.Reset();
                        timer.Value.Start();
                    }
                }
            }
        }

        public void stopTimers()
        {
            foreach (var timer in framesTimer)
            {
                timer.Value.Stop();
            }
        }

        public void restartTimers()
        {
            foreach (var timer in framesTimer)
            {
                timer.Value.Start();
            }
        }
    }
}
