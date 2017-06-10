using System.Threading;
using System;
namespace Tp1
{
    /// <summary>
    /// Class that represents the physical support on the machine.
    /// </summary>
    class PhysicalSupport
    {
        Transmitter transmitter;
        Receiver receiver;
        InterThreadSynchronizer machineSynchronizer;
        bool insertError = false;

        public PhysicalSupport(InterThreadSynchronizer machine, Transmitter t, Receiver r)
        {
            transmitter = t;
            receiver = r;
            machineSynchronizer = machine;
        }

        public void Start(bool error)
        {
            insertError = error;
            Thread Transmition1to2 = new Thread(() => Transmit(machineSynchronizer));
            Transmition1to2.Start();
        }

        /// <summary>
        /// Function that transmit the frame from one machine to another.
        /// </summary>
        public void Transmit(InterThreadSynchronizer machineSynchronizer)
        {
            while (insertError)
            {
                transmitter.stopTimers();
                machineSynchronizer.TransferTrameToDestination(ref insertError);
                transmitter.restartTimers();
                machineSynchronizer.TransferTrameToSource();
            }

            while (true)
            {                
                machineSynchronizer.TransferTrameToDestination(ref insertError);
                machineSynchronizer.TransferTrameToSource();
            }
        }
    }
}
