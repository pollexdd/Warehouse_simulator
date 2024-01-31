namespace WarehouseSimulation
{
    public class Warehouse
    {
        public List<Shelf> Shelves { get; private set; }
        public WarehouseConfiguration Configuration { get; private set; }
        public DeliverySchedule DeliverySchedule { get; private set; }
        public PickupSchedule PickupSchedule { get; private set; }
        public Terminal Terminal { get; private set; }
        public int CurrentSimulationDay { get; private set; }
        public List<Item> ItemHistory { get; set; } = new List<Item>();
        private Simulation simulation;

        public Warehouse(WarehouseConfiguration configuration)

        {
            Configuration = configuration;
            Shelves = new List<Shelf>();
            DeliverySchedule = new DeliverySchedule();
            PickupSchedule = new PickupSchedule();
            Terminal = new Terminal(configuration.TerminalCapacity);
            CurrentSimulationDay = 0;
            simulation = new Simulation();
        }

        public void AddItem(string itemName, GoodsType goodsType)
        {
            Item newItem = new Item(itemName, goodsType);
            Console.WriteLine($"Item '{newItem.Name}' added to the warehouse.");
        }

        public void AddShelf(string shelfId, int capacity, GoodsType goodsType, int terminalToShelfTime, int shelfToTerminalTime)
        {
            Shelf newShelf = Shelf.CreateShelf(shelfId, capacity, goodsType, terminalToShelfTime, shelfToTerminalTime);
            Shelf.AddShelfToWarehouse(Shelves, newShelf);
        }

        public void RemoveShelf(string shelfId)
        {
            Shelf.RemoveShelfFromWarehouse(Shelves, shelfId);
        }

        public void AddDelivery(int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Delivery newDelivery = new Delivery(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newDelivery.ScheduleDelivery(this);
        }

        public void AddWeeklyDelivery(int daysBetweenDeliveries, int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Delivery newWeeklyDelivery = new Delivery(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newWeeklyDelivery.ScheduleWeeklyDelivery(this, daysBetweenDeliveries);
        }

        public void AddPickup(int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Pickup newPickup = new Pickup(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newPickup.SchedulePickup(this);
        }

        public void AddWeeklyPickup(int daysBetweenPickups, int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Pickup newWeeklyPickup = new Pickup(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newWeeklyPickup.ScheduleWeeklyPickup(this, daysBetweenPickups);
        }

        public void ProcessDelivery(Delivery delivery)
        {
            delivery.Process(this);
        }

        public void ProcessPickup(Pickup pickup)
        {
            pickup.Process(this);
        }

        public void PrintItemHistory(string itemName)
        {
            Item.PrintItemHistory(ItemHistory, itemName);
        }
        public void SimulationRun(int daysToSimulate)
        {
            simulation.SimulationRun(this, daysToSimulate);
        }
        public Item FindOrCreateItem(string itemName, GoodsType goodsType)
        {
            Item existingItem = Shelves
                .SelectMany(shelf => shelf.Items)
                .FirstOrDefault(item => item.Name == itemName && item.GoodsType == goodsType);

            if (existingItem != null)
            {
                return existingItem;
            }

            Item newItem = new Item(itemName, goodsType);
            return newItem;
        }
    }
}