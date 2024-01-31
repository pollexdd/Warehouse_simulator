namespace WarehouseSimulation
{
    public class PickupSchedule
    {
        private List<Pickup> pickups = new List<Pickup>();

        public void AddPickup(Pickup pickup)
        {
            pickups.Add(pickup);
        }
        public List<Pickup> GetPickups()
        {
            return pickups;
        }

        public void AddWeeklyPickup(int daysBetweenPickups, Pickup pickup)
        {
            for (int day = pickup.SimulationDay; day <= 7 * 1000; day += daysBetweenPickups)
            {
                pickups.Add(new Pickup(day, pickup.GoodsType, pickup.Quantity, pickup.Item));
            }
        }

    }
}
