using System;
using System.Collections.Generic;
using NutHouseVending.Interfaces;

namespace NutHouseVending
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            var wares = Storage.wares;
            
            vendingMachine.Run(wares);
        }
    }
}
