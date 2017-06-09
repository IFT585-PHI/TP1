using System;

namespace Tp1
{
    class Program
    {
        static void Main(string[] args)
        {

            InterThreadSynchronizer machine1Synchronizer = new InterThreadSynchronizer();
            Inputs inputs = new Inputs();
            inputs.ReadInputs();

            PhysicalSupport support = new PhysicalSupport(machine1Synchronizer);
            //Simulator machine1 = new Simulator(new Transmitter(machine1Synchronizer), new Receiver("test.txt", machine1Synchronizer), inputs);
            //Simulator machine2 = new Simulator(new Transmitter(machine2Synchronizer), new Receiver("test.txt", machine2Synchronizer), inputs);
            Transmitter t = new Transmitter(machine2Synchronizer);
            Receiver r = new Receiver(inputs.DestinationFileName, machine2Synchronizer);
            //support.setMachine1(machine1);
            //support.setMachine2(machine2);
            int errorsCount = 0;
            bool errorMaually = false;
            AskError(ref errorsCount, ref errorMaually);


            //support.Start();
        }

        static void AskError(ref int errorsCount, ref bool errorManually)
        {
            Console.WriteLine("\nHow many frames do you want to insert error(s)?");
            Int32.TryParse(Console.ReadLine(), out errorsCount);

            Console.WriteLine("Do you want the errors to be inserted manualy? (y/n)");
            string r = Console.ReadLine();
            while (r != "y" && r != "n")
                r = Console.ReadLine();


            Console.Write(errorsCount + " frames will be affected ");
            if (r == "y")
            {
                errorManually = true;
                Console.Write("manually.\n");
            }
            else
            {
                Console.Write("randomly.\n");
            }

        }

    }
}
