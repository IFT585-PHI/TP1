using System.Threading;
using System.Threading.Tasks;

namespace Tp1
{
    class PhysicalSupport
    {
        Simulator machine1;
        Simulator machine2;
        Thread Transmition1to2;
        Thread Transmition2to1;

        public PhysicalSupport(InterThreadSynchronizer machine1Synchronizer, InterThreadSynchronizer machine2Synchronizer)
        {
            Transmition1to2 = new Thread(() => Transmit(machine1Synchronizer));
            Transmition2to1 = new Thread(() => Transmit(machine2Synchronizer));
        }

        public void Start()
        {
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

        public void setMachine1(Simulator s)
        {
            machine1 = s;
        }
        public void setMachine2(Simulator s)
        {
            machine2 = s;
        }

    }
}
