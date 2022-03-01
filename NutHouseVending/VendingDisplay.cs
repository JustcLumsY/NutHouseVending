using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NutHouseVending.Interfaces;

namespace NutHouseVending
{
    internal class VendingDisplay
    {
        public MoneyHandler Moneyhandler { get; set; }
        public VendingDisplay()
        {
            Moneyhandler = new MoneyHandler();
            Console.SetWindowSize(80, 35);
        }

        public void VendingMachineDisplay(List<Ware> wares)
        { 
            Console.Clear();
            var totalCoin = MoneyHandler.AmountOfMoney;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine // Header
            (@"
                ╬════════════════════════════════════════════════╬
                ║            >>||Nut House Vending||<<           ║
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

        public static void SetCursorPositionCenter()
        {
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
        }

        public void SelectAProductText()
        {
            var SelectText = "↑ Select a product number ↑";
            Console.SetCursorPosition((Console.WindowWidth - SelectText.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(SelectText);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SoldOut(List<Ware> wares)
        {
            VendingMachineDisplay(wares);
            RedColor();
            Console.WriteLine(wares);
            var soldOutText = "<|SOLD OUT|>";
            Console.SetCursorPosition((Console.WindowWidth - soldOutText.Length) / 2, Console.CursorTop);
            Console.WriteLine(soldOutText);
            Console.ResetColor();
        }

        public void CheckAmountOfMoney(List<Ware> wares, Ware ware)
        {
            NotEnoughMoneyTextTypeAndPriceShow(wares, ware);
            Moneyhandler.InsertCoin();
        }

        private void NotEnoughMoneyTextTypeAndPriceShow(List<Ware> wares, Ware ware)
        {
            var notEnoughMoneyText = "Not enough money, please insert";
            var youPicked = $"{ware.Type}";
            var kr = $"{ware.Price}Kr";
            var Coins = MoneyHandler.AmountOfMoney;
            VendingMachineDisplay(wares);
            Console.SetCursorPosition((Console.WindowWidth - youPicked.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"<{youPicked}> | Coins: {Coins}");
            Console.SetCursorPosition((Console.WindowWidth - notEnoughMoneyText.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(notEnoughMoneyText);
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorPositionCenter();
        }

        public static void WhiteColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void GreenColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void RedColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }


    }
}

