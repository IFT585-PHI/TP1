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
            NetworkReceiver ReseauxRecepteur = new NetworkReceiver(inputs.DestinationFileName);
            Receiver receiver = new Receiver(ReseauxRecepteur, machineSynchronizer, inputs.BufferSize);

            PhysicalSupport support = new PhysicalSupport(machineSynchronizer, transmitter, receiver);
            
            bool insertErrors = Logger.ReadStringChoice("Voulez-vous inserer des erreurs?");

            Thread transmitterThread = new Thread(() => transmitter.Transmitting(inputs));
            Thread receivierThread = new Thread(() => receiver.Receiving());
            transmitterThread.Start();
            receivierThread.Start();

            support.Start(insertErrors);
        }

    }
}
