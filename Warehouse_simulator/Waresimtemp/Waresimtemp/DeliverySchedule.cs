namespace WarehouseSimulation
{
    public class DeliverySchedule
    {
        private List<Delivery> deliveries = new List<Delivery>();

        public void AddDelivery(Delivery delivery)
        {
            deliveries.Add(delivery);
        }
        public void AddWeeklyDelivery(int daysBetweenDeliveries, Delivery delivery)
        {
            for (int day = delivery.SimulationDay; day <= 7 * 1000; day += daysBetweenDeliveries)
            {
                deliveries.Add(new Delivery(day, delivery.GoodsType, delivery.Quantity, delivery.Item));
            }
        }
        public List<Delivery> GetDeliveries()
        {
            return deliveries;
        }

    }
}
