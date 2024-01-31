namespace WarehouseSimulation
{
    public class Simulation
    {
        public int CurrentSimulationDay { get; private set; }

        public void SimulateDay(Warehouse warehouse)
        {
            CurrentSimulationDay++;

            var deliveriesForToday = warehouse.DeliverySchedule.GetDeliveries().Where(delivery => delivery.SimulationDay == CurrentSimulationDay).ToList();
            foreach (var delivery in deliveriesForToday)
            {
                warehouse.ProcessDelivery(delivery);
            }

            var pickupsForToday = warehouse.PickupSchedule.GetPickups().Where(pickup => pickup.SimulationDay == CurrentSimulationDay).ToList();
            foreach (var pickup in pickupsForToday)
            {
                warehouse.ProcessPickup(pickup);
            }

            Console.WriteLine($"Simulation Day: {CurrentSimulationDay}");
            GenerateDailyShelfReport(warehouse);
        }

        public void GenerateDailyShelfReport(Warehouse warehouse)
        {
            Console.WriteLine("Daily Shelf Report:");

            foreach (var shelf in warehouse.Shelves)
            {
                Console.WriteLine($"Shelf '{shelf.Id}' - Capacity: {shelf.Capacity}, Goods Type: {shelf.GoodsType}");

                if (shelf.Items.Any())
                {
                    Console.WriteLine("Items:");

                    Dictionary<string, int> itemCounts = new Dictionary<string, int>();

                    foreach (var item in shelf.Items)
                    {
                        if (itemCounts.ContainsKey(item.Name))
                        {
                            itemCounts[item.Name]++;
                        }
                        else
                        {
                            itemCounts[item.Name] = 1;
                        }
                    }

                    foreach (var itemName in itemCounts.Keys)
                    {
                        int itemCount = itemCounts[itemName];
                        Console.WriteLine($"  - {itemName}");
                    }
                }
                else
                {
                    Console.WriteLine("No items in the shelf.");
                }

                Console.WriteLine();
            }
        }
        public void SimulationRun(Warehouse warehouse, int daysToSimulate)
        {
            for (int day = 1; day <= daysToSimulate; day++)
            {
                SimulateDay(warehouse);
            }


        }


    }
}
