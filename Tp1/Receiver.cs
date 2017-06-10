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
        SortedDictionary<int, Frame> message;
        ReseauxRecepteur coucheReseaux;
        Frame[] receptionBuffer;
        int NakSeqNum = -1;
        int bufferSize;


        public Receiver(ReseauxRecepteur coucheReseaux, InterThreadSynchronizer synchronizer, int bufferSize)
        {
            this.coucheReseaux = coucheReseaux;
            this.synchronizer = synchronizer;
            this.bufferSize = bufferSize;

            message = new SortedDictionary<int, Frame>();
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
<<<<<<< HEAD
=======
                if(trame.type == Type.Fin)
                {
                    Logger.WriteMessage("Fin de la transmition.");
                    String s = BuildMessage();
                    File.WriteAllText(outputpa, s);
                    break;
                }

>>>>>>> 9de5bbd95cf1580a7b4827e1a1c237d4d22f283d

                bool isValid = Hamming.Validate(ref trame.Message);

                if (!isValid)
                {
<<<<<<< HEAD
                    SendInvalideRespone(trame);
                } else {
                    SendValidResponse(trame);
=======
                    Logger.WriteMessage("Trame " + trame.FrameId + "n'est pas valide.");
                    validationCode = Type.Nak;
                }
                else
                {
                    Logger.WriteMessage("Trame " + trame.FrameId + "est valide.");
                    validationCode = Type.Ack;
                    message[trame.FrameId] = trame;
>>>>>>> 9de5bbd95cf1580a7b4827e1a1c237d4d22f283d
                }
            }
        }

        private void waitForFrame()
        {
            while (!synchronizer.ReadyToReadSourceMessage())
            {
            }
        }

        private void SendToSource(int numSeq, Type type)
        {
<<<<<<< HEAD
            Frame trame = new Tp1.Frame();
            trame.FrameId = numSeq;
            trame.type = type;
            while (!synchronizer.TransferTrameToSupportDestination(trame))
            {
            }
=======
            while (!synchronizer.TransferTrameToSupportDestination(trame)) ;
>>>>>>> 9de5bbd95cf1580a7b4827e1a1c237d4d22f283d
        }

        private void SendInvalideRespone(Frame trame)
        {
            NakSeqNum = trame.FrameId;
            SendToSource(NakSeqNum, Type.Nak );

            Console.WriteLine("Trame " + trame.FrameId + "n'est pas valide.");
            Console.WriteLine("Envoi Nak trame #" + trame.FrameId + "to support");
        }

        private void SendValidResponse(Frame trame)
        {

            if (NakSeqNum < 0)
            {
                SendAckOfCurrentFrame(trame);
            }
            else if (trame.FrameId == NakSeqNum)
            {
                SendAckWhenNakFrameIsValid(trame);
            }
            else
            {
                SendAckOfLastValidFrameBeforeNak(trame);

            }
        }

        private void SendAckOfCurrentFrame(Frame trame)
        {
            SendToSource(trame.FrameId, Type.Ack);

            Console.WriteLine("Trame " + trame.FrameId + "est valide.");
            Console.WriteLine("Envoi Ack trame #" + trame.FrameId + "to support");
        }

        private void SendAckWhenNakFrameIsValid(Frame trame)
        {
            int tramePlusHauteRecue = FreeBufferToTheReseau();

            NakSeqNum = -1;
            SendToSource(tramePlusHauteRecue , Type.Ack);

            Console.WriteLine("Trame " + NakSeqNum + "reçu, transmission à la couche réseaux.");
        }

        private void SendAckOfLastValidFrameBeforeNak(Frame trame)
        {
            int sqNum = 0;

            if (NakSeqNum == 0)
            {
                sqNum = 8;
            }
            else
            {
                sqNum = NakSeqNum - 1;
            }
            receptionBuffer[trame.FrameId] = trame;
            SendToSource(sqNum, Type.Ack);

            Console.WriteLine("Trame " + trame.FrameId + "est valide.");
            Console.WriteLine("Trame " + NakSeqNum + "est encore manquante.");
        }

        private int FreeBufferToTheReseau()
        {
<<<<<<< HEAD
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
=======
            List<char> result = new List<char>();
            foreach(KeyValuePair<int, Frame> entry in message)
            {
                result.Add(Hamming.Decode(entry.Value.Message));
            }
            return Util.ListToString(result);
>>>>>>> 9de5bbd95cf1580a7b4827e1a1c237d4d22f283d
        }
    }
}