using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IOrderSenderService
{
    Task SendToWarehouse(IEnumerable<BasketItemViewModel> items);
}
