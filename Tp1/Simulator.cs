using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    class Simulator
    {
        Transmitter transmitter;
        Receiver reciever;

        public Simulator(Transmitter t, Receiver r, PhysicalSupport support)
        {
            transmitter = t;
            reciever = r;
        }

        public void Transmit()
        {

        }

        public void Recieve()
        {

        }
    }
}
