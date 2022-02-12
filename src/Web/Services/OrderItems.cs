namespace Microsoft.eShopWeb.Web.Services;
public class OrderItems
{
    public int OrderId { get; set; }
    public IEnumerable<OrderItem> Items { get; set; }
}
