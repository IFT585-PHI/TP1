using System.Threading;

namespace Tp1
{
    class Program
    {     
        static void Main(string[] args)
        {
            InterThreadSynchronizer machine1Synchronizer = new InterThreadSynchronizer();
            InterThreadSynchronizer machine2Synchronizer = new InterThreadSynchronizer();
            Transmitter transmitter = new Transmitter(machine1Synchronizer);
            Receiver reciever = new Receiver("test.txt", machine1Synchronizer);
            Inputs inputs = new Inputs();
            inputs.ReadInputs();
            PhysicalSupport support = new PhysicalSupport(machine1Synchronizer);

            Thread transmitterThread = new Thread(() => transmitter.Transmitting(inputs));
            Thread receivierThread = new Thread(() => reciever.Receiving());
            transmitterThread.Start();
            receivierThread.Start();
        }
        
    }
}
