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
        PhysicalSupport support;

        public Receiver(string outputPath, PhysicalSupport support)
        {
            sw = new StreamWriter(outputPath);
            decoder = new Decoder();
            this.support = support;
        }

        public void Receiving()
        {
            while
            string message = decoder.Decode(trame, "thingy");
            string code;
            if (String.IsNullOrEmpty(message))
            {
                code = ERROR_CODE;
            } else
            {
                sw.Write(message);
                code = SUCCES_CODE;
            }    

            
        }

       


    }
}
