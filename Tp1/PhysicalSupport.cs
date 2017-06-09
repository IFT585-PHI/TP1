using System.Threading;
using System.Threading.Tasks;

namespace Tp1
{
    class PhysicalSupport
    {
        public PhysicalSupport(InterThreadSynchronizer machine1Synchronizer, InterThreadSynchronizer machine2Synchronizer)
        { 
            Thread Transmition1to2 = new Thread(() => Transmit(machine1Synchronizer));
            Thread Transmition2to1 = new Thread(() => Transmit(machine2Synchronizer));
            Transmition1to2.Start();
            Transmition2to1.Start();
        }

        public void Transmit(InterThreadSynchronizer machineSynchronizer)
        {
            while (true)
            {
                machineSynchronizer.TransferTrameToDestination();
            }
        }
    }
}
