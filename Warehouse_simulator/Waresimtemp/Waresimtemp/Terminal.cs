namespace WarehouseSimulation
{
    public class Terminal
    {
        public List<Item> Items { get; private set; }
        public int Capacity { get; private set; }

        public Terminal(int capacity)
        {
            Capacity = capacity;
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
                Console.WriteLine($"Terminal is at full capacity. Cannot add item '{item.Name}'.");

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
                Console.WriteLine($"Item '{item.Name}' not found in the terminal.");

            }
        }
        public List<Item> GetItems()
        {
            return new List<Item>(Items);
        }
        public void ConfigureTerminal(int capacity)
        {
            Capacity = capacity;
            Console.WriteLine($"Terminal capacity has been configured to {capacity}.");
        }
    }
}
