using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tp1
{
    class Simulator
    {
        Transmitter transmitter;
        Receiver reciever;
        Inputs inputs;

        public Simulator(Transmitter t, Receiver r, Inputs inputs)
        {
            transmitter = t;
            reciever = r;
            this.inputs = inputs;
            Transmit();
            Recieve();
        }

        public void Transmit()
        {
            Thread Transmit = new Thread(() => transmitter.Transmitting(inputs));
            Transmit.Start();
        }

        public void Recieve()
        {
            Thread Recieve = new Thread(() => reciever.Receiving());
            Recieve.Start();
        }
    }
}
