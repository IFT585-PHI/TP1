using System.Threading;
using System.Threading.Tasks;

namespace Tp1
{
    /// <summary>
    /// Class that represents the physical support on the machine.
    /// </summary>
    class PhysicalSupport
    {
        public PhysicalSupport(InterThreadSynchronizer machine1Synchronizer, Transmitter t, Receiver r)
        { 
            Thread Transmition1to2 = new Thread(() => Transmit(machine1Synchronizer));
            Thread Transmition2to1 = new Thread(() => Transmit(machine2Synchronizer));
            Transmition1to2.Start();
            Transmition2to1.Start();
        }

        public void Start()
        {
            while (true)
            {
                TransferTrameToDestination()

            }
        }

        /// <summary>
        /// Function that transmit the frame from one machine to another.
        /// </summary>
        public void Transmit(InterThreadSynchronizer machineSynchronizer)
        {
            while (true)
            {
                machineSynchronizer.TransferTrameToDestination();
            }
        }
    }
}
