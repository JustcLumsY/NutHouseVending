using System;
using System.Collections.Generic;
using System.Threading;
using NutHouseVending.Interfaces;

namespace NutHouseVending
{
    internal class VendingMachine
    {
        public MoneyHandler Moneyhandler { get; set; }
        public VendingDisplay Vendingdisplay { get; set; }
        public Storage storage { get; set; }

        public VendingMachine()
        {
            Moneyhandler = new MoneyHandler();
            Vendingdisplay = new VendingDisplay();
            storage = new Storage();
            storage.InitializeWares();
        }
       
        public void Run(List<Ware> wares)
        {
            Vendingdisplay.VendingMachineDisplay(wares);
            Vendingdisplay.SelectAProductText();
            CheckStorage(wares);
        }

        private void CheckStorage(List<Ware> wares)
        {
            VendingDisplay.SetCursorPositionCenter();
            var userInput = Convert.ToInt32(Console.ReadLine());
                
            if (!storage.CheckStorage((wareEnum) userInput))
            {
                Vendingdisplay.SoldOut(wares);
            }
            else
            {
                Vendingdisplay.VendingMachineDisplay(wares);
                Ware ware = storage.GetWareInfo((wareEnum)userInput, wares);
                var userInput2 = Console.ReadLine();
                switch (userInput2)
                {
                    case "r":
                    case "R":
                        Run(wares);
                        break;
                }

                if (Moneyhandler.HasEnoughMoney(ware.Price))
                {
                    Moneyhandler.SpendMoney(ware.Price);
                }

                while (MoneyHandler.AmountOfMoney < ware.Price)
                {
                    Vendingdisplay.CheckAmountOfMoney(wares);

                    if (MoneyHandler.AmountOfMoney >= ware.Price)
                    {
                        Vendingdisplay.VendingMachineDisplay(wares);
                        ThanksForBuying(ware);
                    }
                }
            }
        }

        private static void ThanksForBuying(Ware ware)
        {
            var wareType = $"<{ware.Type}>";
            var thanksForBuyingText = "↓ Thanks for buying ↓";
            var arrow1 = "|         |";
            Console.SetCursorPosition((Console.WindowWidth - thanksForBuyingText.Length) / 2, Console.CursorTop);
            Console.WriteLine(thanksForBuyingText);
            Sleep500();
            Console.SetCursorPosition((Console.WindowWidth - arrow1.Length) / 2, Console.CursorTop);
            Console.WriteLine(arrow1);
            Sleep500();
            Console.SetCursorPosition((Console.WindowWidth - wareType.Length) / 2, Console.CursorTop);
            VendingDisplay.GreenColor();
            Console.WriteLine(wareType);
            VendingDisplay.WhiteColor();
        }

   

        private static void Sleep500()
        {
            Thread.Sleep(500);
        }

    }
}
