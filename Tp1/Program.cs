﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PhysicalSupport support = new PhysicalSupport();
            Simulator machine1 = new Simulator(new Transmitter(), new Receiver("test.txt"), support);
            Simulator machine2 = new Simulator(new Transmitter(), new Receiver("test.txt"), support);
        }
    }
}
