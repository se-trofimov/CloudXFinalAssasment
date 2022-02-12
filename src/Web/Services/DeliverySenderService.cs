using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DeliveryOrderProcessor.Shared;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Services
{
    public class DeliverySenderService: IDeliverySenderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DeliverySenderService> _logger;
        private HttpClient _httpClient;

        public DeliverySenderService(IHttpClientFactory httpClientFactory, ILogger<DeliverySenderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _httpClient = _httpClientFactory.CreateClient("DeliveryOrderProcessorClient");
        }
        public async Task SendToDelivery(Order order)
        {
            var details = new OrderDetails()
            {
                OrderId = order.Id,
                ShippingAddress = order.ShipToAddress.ToString(),
                FinalPrice = order.Total(),
                ListOfItems = order.OrderItems.Select(x=> new DeliveryOrderProcessor.Shared.OrderItem()
                {
                    CatalogItemId = x.ItemOrdered.CatalogItemId,
                    Quantity = x.Units,
                    CatalogItemName = x.ItemOrdered.ProductName,
                    Price = x.UnitPrice
                })
            };

            var str = JsonSerializer.Serialize(details);
            _logger.LogInformation($"Sending data to OrderDeliveryProcessor: {str}");
            await _httpClient.PostAsync(string.Empty, new StringContent(str, Encoding.UTF8, MediaTypeNames.Application.Json));
        }
    }
}
