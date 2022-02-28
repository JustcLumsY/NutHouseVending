namespace NutHouseVending.Interfaces
{
    internal class Ware
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public wareEnum Type { get; set; }

        public Ware(string name, int price, int amount,  wareEnum type)
        {
            Name = name;
            Price = price;
            Amount = amount;
            Type = type;
        }
    }
}
