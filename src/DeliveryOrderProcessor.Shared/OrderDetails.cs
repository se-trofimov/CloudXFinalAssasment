namespace DeliveryOrderProcessor.Shared
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public string ShippingAddress { get; set; }
        public decimal FinalPrice { get; set; }
        public IEnumerable<OrderItem> ListOfItems { get; set; }
    }
}
