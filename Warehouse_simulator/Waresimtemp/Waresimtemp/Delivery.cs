namespace WarehouseSimulation
{
    public class Delivery
    {
        public int SimulationDay { get; set; }
        public GoodsType GoodsType { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }

        public Delivery(int simulationDay, GoodsType goodsType, int quantity, Item item)
        {
            SimulationDay = simulationDay;
            GoodsType = goodsType;
            Quantity = quantity;
            Item = item;
        }

        public DateTime GetDeliveryTime(DateTime simulationStartDate)
        {
            return simulationStartDate.AddDays(SimulationDay);
        }
        public void ScheduleDelivery(Warehouse warehouse)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Delivery newDelivery = new Delivery(SimulationDay, GoodsType, Quantity, item);
            warehouse.DeliverySchedule.AddDelivery(newDelivery);
            Console.WriteLine($"Delivery scheduled for '{item.Name}' on simulation day {SimulationDay}.");
        }

        public void ScheduleWeeklyDelivery(Warehouse warehouse, int daysBetweenDeliveries)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Delivery newWeeklyDelivery = new Delivery(SimulationDay, GoodsType, Quantity, item);
            warehouse.DeliverySchedule.AddWeeklyDelivery(daysBetweenDeliveries, newWeeklyDelivery);
            Console.WriteLine($"Weekly delivery scheduled for '{item.Name}' every {daysBetweenDeliveries} days starting on simulation day {SimulationDay}.");
        }
        public void Process(Warehouse warehouse)
        {
            Console.WriteLine($"Now processing delivery item: {Item.Name} Count: {Quantity}");
            int remainingQuantity = Quantity;
            int itemid = 1;

            // Add items to the terminal first
            for (int i = 0; i < remainingQuantity; i++)
            {
                string uniqueItemName = $"{Item.Name}_{itemid}";

                Item newItem = new Item(uniqueItemName, GoodsType);

                // Add item to the terminal
                warehouse.Terminal.AddItem(newItem);

                newItem.UpdateLocationHistory($"{newItem.Name} added to Terminal");
                warehouse.ItemHistory.Add(newItem);

                // Retrieve terminalToShelfTime from the respective shelf
                int terminalToShelfTime = warehouse.Shelves
                    .FirstOrDefault(shelf => shelf.GoodsType == GoodsType)
                    ?.TerminalToShelfTime ?? 0;
                itemid++;
            }

            // Transfer items from the terminal to the shelves
            foreach (var shelf in warehouse.Shelves)
            {
                if (shelf.GoodsType == GoodsType)
                {
                    int availableSpace = shelf.Capacity - shelf.Items.Count;
                    int itemsToTransfer = Math.Min(remainingQuantity, availableSpace);

                    for (int i = 0; i < itemsToTransfer; i++)
                    {
                        // Retrieve an item from the terminal
                        Item itemToTransfer = warehouse.Terminal.GetItems().FirstOrDefault();

                        if (itemToTransfer != null)
                        {
                            // Remove item from the terminal
                            warehouse.Terminal.RemoveItem(itemToTransfer);

                            // Add item to the shelf
                            shelf.AddItem(itemToTransfer);

                            itemToTransfer.UpdateLocationHistory($"{itemToTransfer.Name} moved from terminal to Shelf: {shelf.Id}");

                            // Retrieve terminalToShelfTime from the respective shelf
                            int terminalToShelfTime = shelf.TerminalToShelfTime;

                            // Adjust the sleep time as needed
                            System.Threading.Thread.Sleep(terminalToShelfTime);
                            remainingQuantity--;
                        }
                        else
                        {
                            Console.WriteLine($"Insufficient items in the terminal to fulfill the delivery request. Items will not be transferred to the shelves.");
                            return;
                        }
                    }

                    if (remainingQuantity == 0)
                    {
                        break;
                    }
                }
            }

            if (remainingQuantity > 0)
            {
                Console.WriteLine($"Insufficient shelf space of the correct GoodsType to process the entire delivery. Only a partial quantity has been added to the shelves.");
            }
            else
            {
                Console.WriteLine($"Delivery processed successfully.");
            }
        }
    }
}

