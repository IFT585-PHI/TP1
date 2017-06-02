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
        public const string ERROR_CODE = "NAK";
        public const string SUCCES_CODE = "ACK";

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

                Weft trame = synchronizer.GetMessageFromSource();
                string message = decoder.Decode(trame, "thingy");
                string code;

                if (String.IsNullOrEmpty(message))
                {
                    code = ERROR_CODE;
                }
                else
                {
                    sw.Write(message);
                    code = SUCCES_CODE;
                }


            }
        }

       


    }
}
