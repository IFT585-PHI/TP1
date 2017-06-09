namespace Tp1
{
    class Program
    {     
        static void Main(string[] args)
        {
            InterThreadSynchronizer machine1Synchronizer = new InterThreadSynchronizer();
            InterThreadSynchronizer machine2Synchronizer = new InterThreadSynchronizer();
            Inputs inputs = new Inputs();
            inputs.ReadInputs();

            PhysicalSupport support = new PhysicalSupport(machine1Synchronizer, machine2Synchronizer);
            Simulator machine1 = new Simulator(new Transmitter(), new Receiver("test.txt", machine1Synchronizer), inputs);
            Simulator machine2 = new Simulator(new Transmitter(), new Receiver("test.txt", machine2Synchronizer), inputs);
          
        }
        
    }
}
