namespace WarehouseSimulation
{
    public enum GoodsType
    {
        DryGoods,
        Refrigerated,
        Hazardous
    }

    public class Shelf
    {
        public string Id { get; set; }
        public int Capacity { get; set; }
        public GoodsType GoodsType { get; set; }
        public string Area { get; set; }
        public int TerminalToShelfTime { get; set; }
        public int ShelfToTerminalTime { get; set; }

        public List<Item> Items { get; set; }

        public Shelf(string id, int capacity, GoodsType goodsType, int terminalToShelfTime, int shelfToTerminalTime)
        {
            Id = id;
            Capacity = capacity;
            GoodsType = goodsType;
            TerminalToShelfTime = terminalToShelfTime;
            ShelfToTerminalTime = shelfToTerminalTime;
            Items = new List<Item>();
        }


        public void AddItem(Item item)
        {
            if (Items.Count < Capacity)
            {
                Items.Add(item);
            }
            else
            {
                Console.WriteLine($"Shelf '{Id}' is at full capacity. Cannot add item '{item.Name}'.");
            }
        }

        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);

            }
            else
            {
                Console.WriteLine($"Item '{item.Name}' not found on shelf '{Id}'.");
            }
        }
        public static Shelf CreateShelf(string shelfId, int capacity, GoodsType goodsType, int terminalToShelfTime, int shelfToTerminalTime)
        {
            return new Shelf(shelfId, capacity, goodsType, terminalToShelfTime, shelfToTerminalTime);
        }

        public static void AddShelfToWarehouse(List<Shelf> shelves, Shelf newShelf)
        {
            shelves.Add(newShelf);
            Console.WriteLine($"Shelf '{newShelf.Id}' added to the warehouse.");
        }

        public static void RemoveShelfFromWarehouse(List<Shelf> shelves, string shelfId)
        {
            Shelf shelfToRemove = shelves.FirstOrDefault(shelf => shelf.Id == shelfId);

            if (shelfToRemove != null)
            {
                shelves.Remove(shelfToRemove);
                Console.WriteLine($"Shelf '{shelfToRemove.Id}' removed from the warehouse.");
            }
            else
            {
                Console.WriteLine($"Shelf '{shelfId}' not found in the warehouse.");
            }
        }
    }
}


