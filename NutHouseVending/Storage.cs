﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NutHouseVending.Interfaces;

namespace NutHouseVending
{
    internal class Storage 
    {
        public List<Ware> wares { get; set; }
      
        public Storage()
        {
            wares = new List<Ware>();
            
        }
        public void InitializeWares()
        {
            //Drinks
            wares.Add(new Ware("Coca Cola", 25, 0, wareEnum.CocaCola));
            wares.Add(new Ware("Fanta", 22, 10, wareEnum.Fanta));
            wares.Add(new Ware("Pepsi", 20, 10, wareEnum.Pepsi));
            wares.Add(new Ware("Urge", 22, 10, wareEnum.Urge));
            //Chocolates
            wares.Add(new Ware("Mars", 22, 10, wareEnum.Mars));
            wares.Add(new Ware("M&M", 22, 10, wareEnum.MM));
            wares.Add(new Ware("Snickers", 22, 10, wareEnum.Snickers));
            wares.Add(new Ware("Twix", 22, 10, wareEnum.Twix));
            //Snacks
            wares.Add(new Ware("Pringles", 35, 10, wareEnum.Pringles));
            wares.Add(new Ware("Chip", 20, 10, wareEnum.Chip));
            wares.Add(new Ware("Lays", 15, 10, wareEnum.Lays));
            wares.Add(new Ware("Nuts", 28, 10, wareEnum.Nuts));
        }

        public bool CheckStorage(wareEnum type)
        {
            Ware ware = wares.FirstOrDefault(x => x.Type == type);
            if (ware != null)
            {
                return ware.Amount > 0;
            }
            return false;
        }

        public Ware GetWareInfo(wareEnum type, List<Ware> wares)
        {
            Ware ware = wares.FirstOrDefault(x => x.Type == type);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Change product: R           ↓ Your choice ↓");
            Console.WriteLine($"Buy Product: Enter          ---------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"                                 {ware.Type}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"                                 {ware.Price} Kr");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(1500);
            return ware;
        }
        
    }
}