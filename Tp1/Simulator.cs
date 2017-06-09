using System.Threading;

namespace Tp1
{
    ///<Summary> 
    ///Class that represents a machine that will transfer and recieve frames.
    ///</Summary>
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

        ///<Summary> 
        ///Starts the transmitter thread for the machine, that will transmit the input.
        ///</Summary>
        public void Transmit()
        {
            Thread Transmit = new Thread(() => transmitter.Transmitting(inputs));
            Transmit.Start();
        }

        ///<Summary> 
        ///Starts the reciever thread for the machine, that will recieve the input.
        ///</Summary>
        public void Recieve()
        {
            Thread Recieve = new Thread(() => reciever.Receiving());
            Recieve.Start();
        }
    }
}
