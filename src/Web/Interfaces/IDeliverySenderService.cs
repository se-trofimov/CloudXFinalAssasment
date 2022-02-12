using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IDeliverySenderService
    {
        Task SendToDelivery(Order order);
    }
}
