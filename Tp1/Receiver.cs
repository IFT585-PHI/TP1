using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tp1
{
    class Receiver
    {
        StreamWriter sw;
        Decoder decoder;
        InterThreadSynchronizer synchronizer;

        public Receiver(string outputPath, InterThreadSynchronizer synchronizer)
        {
            sw = new StreamWriter(outputPath);
            decoder = new Decoder();
            this.synchronizer = synchronizer;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Receiving()
        {
            while (true)
            {
                while (!synchronizer.ReadyToReadSourceMessage())
                {
                }

                Frame trame = synchronizer.GetMessageFromSource();
                string message = decoder.Decode(trame, "thingy");

                if (String.IsNullOrEmpty(message))
                {
                    trame.type = Type.Nak;
                }
                else
                {
                    sw.Write(message);
                    trame.type = Type.Ack;
                }


            }
        }

       


    }
}
