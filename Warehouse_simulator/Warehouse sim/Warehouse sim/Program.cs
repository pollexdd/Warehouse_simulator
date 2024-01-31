using System;
using Waresimtemp;

namespace Warehouse_sim
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create Warehouse Configuration
            WarehouseConfiguration config = new WarehouseConfiguration
            {
                ShelfCount = 5,
                PackageSize = 10,
                ReceivingToShelfTime = 2,
                ShelfToTerminalTime = 1,
                IsCoolStorage = true,
                IsDryStorage = true,
                IsHazardous = false,
                TerminalCapacity = 20
            };

            // Create Warehouse Instance
            Warehouse myWarehouse = new Warehouse(config);

            // Create Simulation Instance
            Simulation mySimulation = new Simulation(myWarehouse);

            // Example: Add some items going into the warehouse
            myWarehouse.ConfigureDelivery(DateTime.Now.AddHours(1), GoodsType.DryGoods, 50);
            myWarehouse.ConfigureDelivery(DateTime.Now.AddHours(2), GoodsType.Refrigerated, 30);

            // Example: Add some weekly deliveries
            myWarehouse.ConfigureWeeklyDelivery(GoodsType.Hazardous, 100);
            myWarehouse.ConfigureWeeklyDelivery(GoodsType.DryGoods, 200);

            // Example: Add some items going out of the warehouse
            myWarehouse.ConfigurePickup(DateTime.Now.AddHours(3), GoodsType.DryGoods, 20);
            myWarehouse.ConfigurePickup(DateTime.Now.AddHours(4), GoodsType.Refrigerated, 10);

            // Example: Add some weekly pickups
            myWarehouse.ConfigureWeeklyPickup(GoodsType.DryGoods, 30);
            myWarehouse.ConfigureWeeklyPickup(GoodsType.Refrigerated, 15);

            // Run the Simulation for 30 days (simulate a month)
            mySimulation.Run(30);

            // The simulation is complete. You can add more code here if needed.
        }
    }
}
