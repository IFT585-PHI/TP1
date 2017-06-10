﻿using System;
﻿using System.Threading;

namespace Tp1
{
    class Program
    {
        static void Main(string[] args)
        {

            InterThreadSynchronizer machine1Synchronizer = new InterThreadSynchronizer();

            Inputs inputs = new Inputs();
            inputs.ReadInputs();

            Transmitter transmitter = new Transmitter(machine1Synchronizer);
            ReseauxRecepteur ReseauxRecepteur = new ReseauxRecepteur(inputs.DestinationFileName);
            Receiver receiver = new Receiver(ReseauxRecepteur, machine1Synchronizer, inputs.BufferSize);

            PhysicalSupport support = new PhysicalSupport(machine1Synchronizer, transmitter, receiver);
            int errorsCount = 0;
            bool errorMaually = false;
            //AskError(ref errorsCount, ref errorMaually);

            Thread transmitterThread = new Thread(() => transmitter.Transmitting(inputs));
            Thread receivierThread = new Thread(() => receiver.Receiving());
            transmitterThread.Start();
            receivierThread.Start();

            support.Start();
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
