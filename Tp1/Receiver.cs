using System;
using System.Collections.Generic;
using System.IO;

namespace Tp1
{
    /// <summary>
    /// Class that represents the reciever on a machine.
    /// </summary>
    class Receiver
    {
        StreamWriter sw;
        InterThreadSynchronizer synchronizer;
        List<Frame> message;
        ReseauxRecepteur coucheReseaux;
        Frame[] receptionBuffer;
        int NakSeqNum = -1;
        int lastAck = -1;
        int bufferSize;


        public Receiver(ReseauxRecepteur coucheReseaux, InterThreadSynchronizer synchronizer, int bufferSize)
        {
            this.coucheReseaux = coucheReseaux;
            this.synchronizer = synchronizer;
            this.bufferSize = bufferSize;

            message = new List<Frame>();
            receptionBuffer = new Frame[bufferSize];
        }

        /// <summary>
        /// Delagate thread fonction that recieve the frame, if it's available, check for validity and send the message back. When the message is complete,
        /// writes the frame to the file.
        /// </summary>
        public void Receiving()
        {
            while (true)
            {

                waitForFrame();
                Frame trame = synchronizer.GetMessageFromSource();
                if (trame.type == Type.Fin)
                {
                    coucheReseaux.GiveNewFrame(trame);

                }
                else
                {
                    bool isValid = Hamming.Validate(ref trame.Message);

                    if (!isValid)
                    {
                        //NakSeqNum = trame.FrameId;
                        SendToSource(trame.FrameId, Type.Nak);
                    }
                    else
                    {
                        if (lastAck + 1 != trame.FrameId)
                        {
                            message.Add(trame);
                        }
                        else
                        {
                            coucheReseaux.GiveNewFrame(trame);

                            while (message.Count != 0)
                            {
                                if (Hamming.Validate(ref message[0].Message))
                                {
                                    coucheReseaux.GiveNewFrame(message[0]);
                                    message.RemoveAt(0);
                                }
                                else
                                {
                                    SendToSource(message[0].FrameId, Type.Nak);
                                }
                            }
                            SendToSource(trame.FrameId, Type.Ack);
                            lastAck = trame.FrameId;
                        }
                    }
                }
            }
        }

        private void waitForFrame()
        {
            while (!synchronizer.ReadyToReadSourceMessage()) ;
        }

        private void SendToSource(int numSeq, Type type)
        {
            Frame trame = new Tp1.Frame();
            trame.FrameId = numSeq;
            trame.type = type;
            while (!synchronizer.TransferTrameToSupportDestination(trame)) ;
        }

        private void SendInvalideRespone(Frame trame)
        {
            NakSeqNum = trame.FrameId;
            SendToSource(NakSeqNum, Type.Nak);

            Console.WriteLine("Trame " + trame.FrameId + "n'est pas valide.");
            Console.WriteLine("Envoi Nak trame #" + trame.FrameId + "to support");
        }

        private int FreeBufferToTheReseau()
        {
            int tramePlusHauteRecue = 0;

            for (int i = 0; i < bufferSize; i++)
            {
                if (receptionBuffer[i] != null)
                {
                    coucheReseaux.GiveNewFrame(receptionBuffer[i]);
                    tramePlusHauteRecue = i;
                }
            }
            receptionBuffer = new Frame[bufferSize];

            return tramePlusHauteRecue;
        }
    }
}