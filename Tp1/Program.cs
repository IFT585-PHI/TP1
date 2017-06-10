﻿using System;
﻿using System.Threading;

namespace Tp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Inputs inputs = new Inputs();
            inputs.ReadInputs();

            InterThreadSynchronizer machineSynchronizer = new InterThreadSynchronizer();            

            Transmitter transmitter = new Transmitter(machineSynchronizer);
            Receiver receiver = new Receiver(inputs.DestinationFileName, machineSynchronizer);

            PhysicalSupport support = new PhysicalSupport(machineSynchronizer, transmitter, receiver);

            bool insertErrors = AskError();

            Thread transmitterThread = new Thread(() => transmitter.Transmitting(inputs));
            Thread receivierThread = new Thread(() => receiver.Receiving());
            transmitterThread.Start();
            receivierThread.Start();

            support.Start();
        }

        static bool AskError()
        {
            Console.WriteLine("Voulez-vous inserer des erreurs? (y/n)");
            string r = Console.ReadLine();

            while (r != "y" && r != "n")
                r = Console.ReadLine();
            
            if (r == "y")
            {
                return true;
            }

            return false;
        }

    }
}
