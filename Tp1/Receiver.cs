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

        public Receiver(string outputPath, InterThreadSynchronizer synchronizer)
        {
            sw = new StreamWriter(outputPath);
            this.synchronizer = synchronizer;
        }

        /// <summary>
        /// Delagate thread fonction that recieve the frame, if it's available, check for validity and send the message back. When the message is complete,
        /// writes the frame to the file.
        /// </summary>
        public void Receiving()
        {
            while (true)
            {
                while (!synchronizer.ReadyToReadSourceMessage())
                {
                }

                Frame trame = synchronizer.GetMessageFromSource();
                if(trame.type == Type.Fin)
                {
                    Console.WriteLine("Fin de la transmition.");
                    sw.Write(BuildMessage());
                    break;
                }

                bool isValid = Hamming.Validate(trame.Message.ToString());
                Type validationCode;
                if (!isValid)
                {
                    Console.WriteLine("Trame " + trame.FrameId + "n'est pas valide.");
                    Console.WriteLine("Envoi Nak trame #" + trame.FrameId + "to support");
                    validationCode = Type.Nak;
                }
                else
                {
                    Console.WriteLine("Trame " + trame.FrameId + "est valide.");
                    Console.WriteLine("Envoi Ack trame #" + trame.FrameId + "to support");
                    validationCode = Type.Ack;
                    message[trame.FrameId] = trame;
                }
                Frame response = new Frame();
                response.type = validationCode;
                response.FrameId = trame.FrameId;
                SendToSource(response);
            }
        }

        private void SendToSource(Frame trame)
        {
            while (!synchronizer.TransferTrameToSupportSource(trame))
            {
            }
        }

        private string BuildMessage()
        {
            string binary = "";
            foreach(Frame trame in message)
            {
                binary += trame.Message;
            }
            return Util.BinaryToChar(binary);
        }
    }
}