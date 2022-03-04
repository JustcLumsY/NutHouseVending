using System;
using System.Collections.Generic;
using System.Drawing;
using NutHouseVending.Interfaces;

namespace NutHouseVending
{
    internal class VendingDisplay
    {
        public MoneyHandler Moneyhandler { get;}
        public VendingDisplay()
        {
            Moneyhandler = new MoneyHandler();
            Console.SetWindowSize(80, 45);
        }

        public void VendingMachineDisplay(List<Ware> wares)
        { 
            Console.Clear();
            TextColor(ConsoleColor.Cyan);
            Console.WriteLine // Header
            ($@"
            ══════════════════════════════════════════════════════════
            ║║║ ╬════════════════════════════════════════════════╬ ║║║
            ║║║ ║            >>||Nut House Vending||<<           ║ ║║║
            ║║║ ╬════════════════════════════════════════════════╬ ║║║
            ══════════════════════════════════════════════════════════    
            ");
            TextColor(ConsoleColor.White);
            Console.WriteLine // Content
            (@$"
            ══════════════════════════════════════════════════════════
            ║║║ ╬════════════════════════════════════════════════╬ ║║║     
            ║║║ ║  <{wares[0].Type}>  |  <{wares[1].Type}>  |  <{wares[2].Type}> |  <{wares[3].Type}>  ║ ║║║    
            ║║║ ║     {wares[0].Price}Kr     |    {wares[1].Price}Kr   |   {wares[2].Price}Kr   |    {wares[3].Price}Kr  ║ ║║║   
            ║║║ ║  Nr: {(int)wares[0].Type}       |  Nr: {(int)wares[1].Type}    |  Nr: {(int)wares[2].Type}   |  Nr: {(int)wares[3].Type}   ║ ║║║ 
            ║║║ ║              |           |          |          ║ ║║║
            ║║║ ║------------------------------------------------║ ║║║
            ║║║ ║    <{wares[4].Type}>    |   <{wares[5].Type}>    |<{wares[6].Type}>|  <{wares[7].Type}>  ║ ║║║
            ║║║ ║     {wares[4].Price}Kr     |    {wares[5].Price}Kr   |   {wares[6].Price}Kr   |   {wares[7].Price}Kr   ║ ║║║
            ║║║ ║  Nr: {(int)wares[4].Type}       |  Nr: {(int)wares[5].Type}    |  Nr: {(int)wares[6].Type}   | Nr: {(int)wares[7].Type}    ║ ║║║
            ║║║ ║              |           |          |          ║ ║║║       
            ║║║ ║------------------------------------------------║ ║║║
            ║║║ ║  <{wares[8].Type}>  |  <{wares[9].Type}>   |  <{wares[10].Type}>  |  <{wares[11].Type}>  ║ ║║║
            ║║║ ║     {wares[8].Price}Kr     |   {wares[9].Price}Kr    |   {wares[10].Price}Kr   |   {wares[11].Price}Kr   ║ ║║║
            ║║║ ║  Nr: {(int)wares[8].Type}      |  Nr: {(int)wares[9].Type}    |  Nr: {(int)wares[10].Type}  |  Nr: {(int)wares[11].Type}  ║ ║║║
            ║║║ ║              |           |          |          ║ ║║║
            ║║║ ╬════════════════════════════════════════════════╬ ║║║
            ══════════════════════════════════════════════════════════
            ║║║════════════════════════════════════════════════════║║║
            ══════════════════════════════════════════════════════════   

            ");
        }

        public void SelectAProductText()
        {
            var SelectText = "↑ Select a product number ↑";
            Console.SetCursorPosition((Console.WindowWidth - SelectText.Length) / 2, Console.CursorTop);
            TextColor(ConsoleColor.Cyan);
            Console.WriteLine(SelectText);
            TextColor(ConsoleColor.White);
        }
        public void CheckAmountOfMoney(List<Ware> wares, Ware ware)
        {
            NotEnoughMoneyTextTypeAndPriceShow(wares, ware);
            Moneyhandler.InsertCoin();
        }
        private void NotEnoughMoneyTextTypeAndPriceShow(List<Ware> wares, Ware ware)
        {
            var notEnoughMoneyText = "<Not enough money, please insert>";
            var youPicked = $"{ware.Type}";
            var Coins = MoneyHandler.AmountOfMoney;
            VendingMachineDisplay(wares);
            if (Coins <= 0) 
            {
                Console.SetCursorPosition((Console.WindowWidth - notEnoughMoneyText.Length) / 2, Console.CursorTop);
                TextColor(ConsoleColor.Red);
                Console.WriteLine($"{notEnoughMoneyText}");
            }
            TextColor(ConsoleColor.Cyan);
            Console.WriteLine($"                                 ↓ Your choice ↓");
            TextColor(ConsoleColor.White);
            Console.WriteLine($"                                 ---------------");
            TextColor(ConsoleColor.Green);
            Console.SetCursorPosition((Console.WindowWidth - youPicked.Length) / 2, Console.CursorTop);
            Console.WriteLine($"<{youPicked}>");
            TextColor(ConsoleColor.Cyan);
            Console.SetCursorPosition((Console.WindowWidth - 35) / 2, Console.CursorTop);
            Console.WriteLine($"          Coins: {Coins}Kr");
            TextColor(ConsoleColor.White);
            SetCursorPositionCenter(); 
        }
        public void SoldOut(List<Ware> wares)
        {
            VendingMachineDisplay(wares);
            TextColor(ConsoleColor.Red);
            Console.WriteLine(wares);
            var soldOutText = "<|SOLD OUT|>";
            Console.SetCursorPosition((Console.WindowWidth - soldOutText.Length) / 2, Console.CursorTop);
            Console.WriteLine(soldOutText);
            Console.ResetColor();
        }
 
        public static void SetCursorPositionCenter()
        {
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
        }
        public static void TextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}

