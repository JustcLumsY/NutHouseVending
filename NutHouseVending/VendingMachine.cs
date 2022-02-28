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

        public Random rnd = new Random();

        

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
            KickVendingMachine();
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

        private void ThanksForBuying(Ware ware)
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
            ProductGotStuck();
            Console.SetCursorPosition((Console.WindowWidth - wareType.Length) / 2, Console.CursorTop);
            VendingDisplay.GreenColor();
            Console.WriteLine(wareType);
            VendingDisplay.WhiteColor();
            Console.ReadLine();
        }
       
        public void ProductGotStuck()
        {
            var test = storage.wares;
            int randomStuck = rnd.Next(1, 100);
            if (randomStuck > 10)
            {
                BrokenVendingMachine(test);
                var machineGotStuck = "The machine made a noise and stopped working!";
                var chooseBrokenOptionText1 = "1: Kick the Machine in anger";
                var chooseBrokenOptionText2 = "2: Punch the Machine in hope of something";
                var chooseBrokenOptionText3 = "3: Slap it gently";
                Console.SetCursorPosition((Console.WindowWidth - machineGotStuck.Length) / 2, Console.CursorTop);
                Console.WriteLine(machineGotStuck);
                Thread.Sleep(1000);
                Console.WriteLine("                  -------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth - chooseBrokenOptionText1.Length) / 2, Console.CursorTop);
                Console.WriteLine(chooseBrokenOptionText1);
                Console.SetCursorPosition((Console.WindowWidth - chooseBrokenOptionText2.Length) / 2, Console.CursorTop);
                Console.WriteLine(chooseBrokenOptionText2);
                Console.SetCursorPosition((Console.WindowWidth - chooseBrokenOptionText3.Length) / 2, Console.CursorTop);
                Console.WriteLine(chooseBrokenOptionText3);
                Console.ForegroundColor = ConsoleColor.White;

                var userInputBrokenMachine = Console.ReadLine();
                switch (userInputBrokenMachine)
                {
                    case "1":
                        KickVendingMachine();
                        break;
                    case "2":
                        PunchVendingMachine();
                        break;
                    case "3":
                        SlapVendingMachine();
                        break;
                }
            }
        }
        public void SlapVendingMachine()
        {
            int randomSlap = rnd.Next(1, 100);
            if (randomSlap > 80)
            {
                Console.WriteLine("Slap");
            }
        }

        public void PunchVendingMachine()
        {
            int randomPunch = rnd.Next(1, 100);
            if (randomPunch > 50)
            {
                Console.WriteLine("Punch");
            }
        }

        public void KickVendingMachine()
        {
            int randomKick = rnd.Next(1, 100);
            if (randomKick > 25)
            {
                var kickText = "You Kicked the machine";
                var alarmText = "An alarm went off!";
                Console.SetCursorPosition((Console.WindowWidth - kickText.Length) / 2, Console.CursorTop);
                Console.WriteLine(kickText);
                Thread.Sleep(1500);
                Console.SetCursorPosition((Console.WindowWidth - alarmText.Length) / 2, Console.CursorTop);
                Console.WriteLine(alarmText);
                VendingAlarm();
            }
        }

        private void VendingAlarm()
        {
            for (int i = 5; i > 0; i++)
            {
                Console.Beep();
              
            }
        }

        public void BrokenVendingMachine(List<Ware> wares)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine // Header
            (@"
                ╬════════════════════════════════════════════════╬
                ║               >>|| Error 404 ||<<              ║
                ╬════════════════════════════════════════════════╬
            ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine // Content
            (@$"
                ╬════════════════════════════════════════════════╬     
                ║  <{wares[0].Type}>  |  <{wares[1].Type}>  |  <{wares[2].Type}> |  <{wares[3].Type}>  ║    
                ║     {wares[0].Price}Kr     |    {wares[1].Price}Kr   |   {wares[2].Price}Kr   |    {wares[3].Price}Kr  ║   
                ║  Nr: {(int)wares[0].Type}       |  Nr: {(int)wares[1].Type}    |  Nr: {(int)wares[2].Type}   |  Nr: {(int)wares[3].Type}   ║ 
                ║              |           |          |          ║
                ║------------------------------------------------║
                ║    <{wares[4].Type}>    |   <{wares[5].Type}>    |<{wares[6].Type}>|  <{wares[7].Type}>  ║
                ║     {wares[4].Price}Kr     |    {wares[5].Price}Kr   |   {wares[6].Price}Kr   |   {wares[7].Price}Kr   ║
                ║  Nr: {(int)wares[4].Type}       |  Nr: {(int)wares[5].Type}    |  Nr: {(int)wares[6].Type}   | Nr: {(int)wares[7].Type}    ║
                ║              |           |          |          ║       
                ║------------------------------------------------║
                ║  <{wares[8].Type}>  |  <{wares[9].Type}>   |  <{wares[10].Type}>  |  <{wares[11].Type}>  ║
                ║     {wares[8].Price}Kr     |   {wares[9].Price}Kr    |   {wares[10].Price}Kr   |   {wares[11].Price}Kr   ║
                ║  Nr: {(int)wares[8].Type}      |  Nr: {(int)wares[9].Type}    |  Nr: {(int)wares[10].Type}  |  Nr: {(int)wares[11].Type}  ║
                ║              |           |          |          ║
                ╬════════════════════════════════════════════════╬
                                                           
            ");
        }

        //Kick, Slap, Punch methods.
        // Sjanse til å "Sette fast en f.eks flaske"
        // så må man ta "KickTheMachine();" for å få ut varen.

        private static void Sleep500()
        {
            Thread.Sleep(500);
        }

    }
}
