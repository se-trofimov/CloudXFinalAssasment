using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.Web.Services
{
    public class OrderSenderService : IOrderSenderService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public OrderSenderService(string connectionString, string queueName)
        {
            _client = new ServiceBusClient(connectionString);
            _sender = _client.CreateSender(queueName);
        }

        public async Task SendToWarehouse(IEnumerable<BasketItemViewModel> items)
        {
            
            var orderItems =
                items.Select(x => new OrderItem() { CatalogItemId = x.CatalogItemId, Quantity = x.Quantity })
                    .ToArray();
            var message = new OrderItemsMessage() {Items = orderItems};
            await _sender.SendMessageAsync(new ServiceBusMessage(new BinaryData(message)));
        }
    }
}
