namespace WarehouseSimulation
{
    public class Pickup
    {
        public int SimulationDay { get; set; }
        public GoodsType GoodsType { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }

        public Pickup(int simulationDay, GoodsType goodsType, int quantity, Item item)
        {
            SimulationDay = simulationDay;
            GoodsType = goodsType;
            Quantity = quantity;
            Item = item;
        }

        public DateTime GetPickupTime(DateTime simulationStartDate)
        {
            return simulationStartDate.AddDays(SimulationDay);
        }
        public void SchedulePickup(Warehouse warehouse)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Pickup newPickup = new Pickup(SimulationDay, GoodsType, Quantity, item);
            warehouse.PickupSchedule.AddPickup(newPickup);
            Console.WriteLine($"Pickup scheduled for '{item.Name}' on simulation day {SimulationDay}.");
        }

        public void ScheduleWeeklyPickup(Warehouse warehouse, int daysBetweenPickups)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Pickup newWeeklyPickup = new Pickup(SimulationDay, GoodsType, Quantity, item);
            warehouse.PickupSchedule.AddWeeklyPickup(daysBetweenPickups, newWeeklyPickup);
            Console.WriteLine($"Weekly pickup scheduled for '{item.Name}' every {daysBetweenPickups} days starting on simulation day {SimulationDay}.");
        }

        public void Process(Warehouse warehouse)
        {
            int remainingQuantity = Quantity;
            Console.WriteLine($"Now processing pickup item: {Item.Name} Count: {Quantity}");

            foreach (var shelf in warehouse.Shelves)
            {
                if (shelf.GoodsType == GoodsType)
                {
                    var shelfItems = shelf.Items.Where(item => item.GoodsType == GoodsType).ToList();
                    foreach (var item in shelfItems.Take(remainingQuantity))
                    {
                        shelf.RemoveItem(item);
                        remainingQuantity--;

                        warehouse.Terminal.AddItem(item);
                        item.UpdateLocationHistory($"{item.Name} Moved to Terminal from Shelf: {shelf.Id}");

                        System.Threading.Thread.Sleep(shelf.ShelfToTerminalTime);
                    }

                    if (remainingQuantity == 0)
                    {
                        break;
                    }
                }
            }

            foreach (var item in warehouse.Terminal.GetItems().Take(Quantity))
            {
                warehouse.Terminal.RemoveItem(item);
                item.UpdateLocationHistory($"{item.Name} has been sent out of the warehouse");
            }

            if (remainingQuantity == 0)
            {
                Console.WriteLine($"Pickup processed successfully.");
            }
            else
            {
                Console.WriteLine($"Insufficient items in the shelves to fulfill the pickup request. Items will not be removed from the terminal.");
            }
        }
    }
}
