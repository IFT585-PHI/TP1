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

<<<<<<< HEAD
            Transmitter transmitter = new Transmitter(machine1Synchronizer);
            ReseauxRecepteur ReseauxRecepteur = new ReseauxRecepteur(inputs.DestinationFileName);
            Receiver receiver = new Receiver(ReseauxRecepteur, machine1Synchronizer, inputs.BufferSize);

            PhysicalSupport support = new PhysicalSupport(machine1Synchronizer, transmitter, receiver);
            int errorsCount = 0;
            bool errorMaually = false;
            //AskError(ref errorsCount, ref errorMaually);
=======
            InterThreadSynchronizer machineSynchronizer = new InterThreadSynchronizer();            

            Transmitter transmitter = new Transmitter(machineSynchronizer);
            Receiver receiver = new Receiver(inputs.DestinationFileName, machineSynchronizer);

            PhysicalSupport support = new PhysicalSupport(machineSynchronizer, transmitter, receiver);

            bool insertErrors = Logger.ReadStringChoice("Voulez-vous inserer des erreurs?");
>>>>>>> 9de5bbd95cf1580a7b4827e1a1c237d4d22f283d

            Thread transmitterThread = new Thread(() => transmitter.Transmitting(inputs));
            Thread receivierThread = new Thread(() => receiver.Receiving());
            transmitterThread.Start();
            receivierThread.Start();

            support.Start(insertErrors);
        }

    }
}
