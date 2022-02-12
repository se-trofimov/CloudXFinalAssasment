namespace DeliveryOrderProcessor.Shared;
public class OrderItem
{
    public int CatalogItemId { get; set; }
    public string CatalogItemName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
