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
        string outputpa;

        public Receiver(string outputPath, InterThreadSynchronizer synchronizer)
        {
            outputpa = outputPath;
            this.synchronizer = synchronizer;
            message = new SortedDictionary<int, Frame>();
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
                    String s = BuildMessage();
                    File.WriteAllText(outputpa, s);
                    break;
                }


                bool isValid = Hamming.Validate(ref trame.Message);
                Type validationCode;
                if (!isValid)
                {
                 //   Console.WriteLine("Trame " + trame.FrameId + "n'est pas valide.");
                  //  Console.WriteLine("Envoi Nak trame #" + trame.FrameId + "to support");
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
            while (!synchronizer.TransferTrameToSupportDestination(trame))
            {
            }
        }

        private string BuildMessage()
        {
            string result = string.Empty;
            foreach(KeyValuePair<int, Frame> entry in message)
            {
                result += Hamming.Decode(entry.Value.Message);
            }
            return result;
        }
    }
}