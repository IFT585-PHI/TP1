using System.Collections.Generic;
using System.IO;

namespace Tp1
{
    /// <summary>
    /// Class that represents the reciever on a machine.
    /// </summary>
    class Receiver
    {
        public const int END_CODE = 666;

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
                if(trame.FrameId == END_CODE)
                {
                    sw.Write(BuildMessage());
                }

                bool isValid = Hamming.Validate(trame.Message.ToString());
                Type validationCode;
                if (!isValid)
                {
                    validationCode = Type.Nak;
                }
                else
                {
                    validationCode = Type.Ack;
                    message[trame.FrameId] = trame;
                    trame.type = Type.Nak;
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
            synchronizer.TransferTrameToSupportSource(trame);
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
