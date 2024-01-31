namespace WarehouseSimulation
{
    public class Item
    {
        public string Name { get; set; }
        public GoodsType GoodsType { get; set; }
        public List<string> LocationHistory { get; private set; }

        public Item(string name, GoodsType goodsType)
        {
            Name = name;
            GoodsType = goodsType;
            LocationHistory = new List<string>();
        }
        public void UpdateLocationHistory(string newLocation)
        {
            LocationHistory.Add(newLocation);
        }
        public void PrintHistory()
        {
            Console.WriteLine($"History for item '{Name}':");

            foreach (var location in LocationHistory)
            {
                Console.WriteLine($"  - {location}");
            }
        }

        public static Item FindItemByName(List<Item> itemHistory, string itemName)
        {
            return itemHistory.FirstOrDefault(item => item.Name == itemName);
        }

        public static void PrintItemHistory(List<Item> itemHistory, string itemName)
        {
            Item item = FindItemByName(itemHistory, itemName);

            if (item != null)
            {
                item.PrintHistory();
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' not found in the warehouse.");
            }
        }
    }
}
