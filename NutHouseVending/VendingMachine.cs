using System;
using System.Collections.Generic;
using System.Threading;
using NutHouseVending.Interfaces;

namespace NutHouseVending
{
    internal class VendingMachine
    {
        public MoneyHandler Moneyhandler { get;}
        public VendingDisplay Vendingdisplay { get; }
        public Storage storage { get; }
        public Random Rnd = new Random();
        

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
            CheckEnoughCoinsAndStorage(wares);
            KickVendingMachine();
        }

        private void CheckEnoughCoinsAndStorage(List<Ware> wares)
        {
            //UserInput in Int
            VendingDisplay.SetCursorPositionCenter();
            var userInput = Convert.ToInt32(Console.ReadLine());

            //Check storage
            if (!storage.CheckStorage((wareEnum) userInput)) { Vendingdisplay.SoldOut(wares); }

           //Ware info and If EnoughMoney, SpendMoney
            Vendingdisplay.VendingMachineDisplay(wares);
            var ware = storage.GetWareInfo((wareEnum)userInput, wares);
            if (Moneyhandler.HasEnoughMoney(ware.Price)) { Moneyhandler.SpendMoney(ware.Price); }

            //Reset product
            var userInput2 = Console.ReadLine();
            if (userInput2 is "R" or "r") { Console.Clear(); Run(wares); }
            
            //WHILE NOT ENOUGH MONEY
            WhileNotEnoughMoneyCheckOrSpendMoney(wares, ware);
        }

        private void WhileNotEnoughMoneyCheckOrSpendMoney(List<Ware> wares, Ware ware)
        {
            while (MoneyHandler.AmountOfMoney < ware.Price)
            {
                Vendingdisplay.CheckAmountOfMoney(wares, ware);
                if (MoneyHandler.AmountOfMoney < ware.Price) continue;
                Vendingdisplay.VendingMachineDisplay(wares);
                Moneyhandler.SpendMoney(ware.Price);
                var coinsBackText = $"Coins back: {MoneyHandler.AmountOfMoney}";
                Console.SetCursorPosition((Console.WindowWidth - coinsBackText.Length) / 2, Console.CursorTop);
                VendingDisplay.TextColor(ConsoleColor.Green);
                Console.WriteLine(coinsBackText);
                VendingDisplay.TextColor(ConsoleColor.Cyan);
                ThanksForBuyingText(ware);
            }
        }

        //BROKEN MACHINE Methods
        public void BrokenVendingMachine()
        {
            var wares = Storage.wares;
            Console.Clear();
            HeaderColor(ConsoleColor.Red);
            Console.WriteLine // Header
            (@"
                ╬════════════════════════════════════════════════╬
                ║               >>|| Error 404 ||<<              ║
                ╬════════════════════════════════════════════════╬
            ");
            Thread.Sleep(500);
            HeaderColor(ConsoleColor.White);
            for (var i = 0; i < 2; i++)
            {
                Console.Clear();
                HeaderColor(ConsoleColor.Red);
                Thread.Sleep(500);
                HeaderColor(ConsoleColor.White);
                Thread.Sleep(500);
                HeaderColor(ConsoleColor.Red);
            }
            HeaderColor(ConsoleColor.White);
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
            Thread.Sleep(1000);
        }
        public void ProductGotStuck()
        {
            int randomStuck = Rnd.Next(1, 100);
            if (randomStuck < 80)
            {
                BrokenVendingMachine();
                StuckTextAndAlignText();
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
        public void KickVendingMachine()
        {
            var randomKick = Rnd.Next(1, 100);
            if (randomKick >= 30) return;
            KickMachineTextAndTextAlign();
            VendingAlarm();
        }
        public void PunchVendingMachine()
        {
            var wares = Storage.wares;
            var randomPunch = Rnd.Next(1, 100);
            if (randomPunch < 50)
            {
                var punchText = "You punched a hole in the glass and took your product";
                Vendingdisplay.VendingMachineDisplay(wares);
                Console.SetCursorPosition((Console.WindowWidth - punchText.Length) / 2, Console.CursorTop);
                VendingDisplay.TextColor(ConsoleColor.Green);
                Console.WriteLine(punchText);
                Thread.Sleep(1500);
                Run(wares);
            }
            else
            {
                var punchFailText = "You punched the machine... Nothing happened";
                Console.SetCursorPosition((Console.WindowWidth - punchFailText.Length) / 2, Console.CursorTop);
                Console.WriteLine(punchFailText);
                Run(wares);
            }
        }
        public void SlapVendingMachine()
        {
            var wares = Storage.wares;
            var randomSlap = Rnd.Next(1, 100);
            if (randomSlap < 80)
            {
                var slapText = "<You slapped the machine and saw your product fell down>";
                Console.SetCursorPosition((Console.WindowWidth - slapText.Length) / 2, Console.CursorTop);
                Console.WriteLine(slapText);
                Thread.Sleep(1500);
                Run(wares);
            }
            else
            {
                var slapFailText = "<Your slap was weak and did nothing>";
                Vendingdisplay.VendingMachineDisplay(wares);
                Console.SetCursorPosition((Console.WindowWidth - slapFailText.Length) / 2, Console.CursorTop);
                Console.WriteLine(slapFailText);
                Thread.Sleep(1500);
                Run(wares);
            }
        }
        private static void StuckTextAndAlignText()
        {
            VendingDisplay.TextColor(ConsoleColor.White);
            var machineGotStuck = "<The machine made a noise and stopped working>";
            var chooseBrokenOptionText1 = "1: Kick the Machine in anger";
            var chooseBrokenOptionText2 = "2: Punch the Machine in hope of something";
            var chooseBrokenOptionText3 = "3: Slap it gently";
            Console.SetCursorPosition((Console.WindowWidth - machineGotStuck.Length) / 2, Console.CursorTop);
            VendingDisplay.TextColor(ConsoleColor.White);
            Console.WriteLine(machineGotStuck);
            Thread.Sleep(1000);
            Console.WriteLine("                  -------------------------------------------");
            VendingDisplay.TextColor(ConsoleColor.Red);
            Console.SetCursorPosition((Console.WindowWidth - chooseBrokenOptionText1.Length) / 2, Console.CursorTop);
            Console.WriteLine(chooseBrokenOptionText1);
            Console.SetCursorPosition((Console.WindowWidth - chooseBrokenOptionText2.Length) / 2, Console.CursorTop);
            Console.WriteLine(chooseBrokenOptionText2);
            Console.SetCursorPosition((Console.WindowWidth - chooseBrokenOptionText3.Length) / 2, Console.CursorTop);
            Console.WriteLine(chooseBrokenOptionText3);
            VendingDisplay.TextColor(ConsoleColor.White);
            VendingDisplay.SetCursorPositionCenter();
        }
        private void KickMachineTextAndTextAlign()
        {
            var wares = Storage.wares;
            var kickText = "You Kicked the machine";
            var alarmText = "An alarm went off!";
            Console.SetCursorPosition((Console.WindowWidth - kickText.Length) / 2, Console.CursorTop);
            Console.WriteLine(kickText);
            Thread.Sleep(1500);
            Console.SetCursorPosition((Console.WindowWidth - alarmText.Length) / 2, Console.CursorTop);
            Console.WriteLine(alarmText);
            Thread.Sleep(1500);
            Run(wares);
        }
        private void VendingAlarm()
        {
            var wares = Storage.wares;
            for (var i = 15; i > 0; i--)
            {
                Console.Beep();
                if (i == 1)
                {
                    Thread.Sleep(1500);
                    Run(wares);
                }
            }
        }
        private void ThanksForBuyingText(Ware ware)
        {
            var wares = Storage.wares;
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
            VendingDisplay.TextColor(ConsoleColor.Green);
            Console.WriteLine(wareType);
            VendingDisplay.TextColor(ConsoleColor.White);
            Thread.Sleep(3000);
            MoneyHandler.AmountOfMoney = 0;
            Run(wares);
        }
        private static void HeaderColor(ConsoleColor color)
        {
            
            Console.Clear();
            Console.ForegroundColor = color;
            VendingDisplay.TextColor(color);
            Console.WriteLine
            (@"
                ╬════════════════════════════════════════════════╬
                ║               >>|| Error 404 ||<<              ║
                ╬════════════════════════════════════════════════╬
            ");
        }
        private static void Sleep500()
        {
            Thread.Sleep(500);
        }
        //private void CheckKeyEnter()
        //{
        //    var wares = Storage.wares;
        //    if (Console.ReadKey().Key == ConsoleKey.Enter)
        //    {
        //        CheckEnoughCoinsAndStorage(wares);
        //    }
        //}
    }
}
