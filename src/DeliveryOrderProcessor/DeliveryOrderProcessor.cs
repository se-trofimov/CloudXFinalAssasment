using System;
using System.Text.Json;
using System.Threading.Tasks;
using DeliveryOrderProcessor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DeliveryOrderProcessor
{
    public static class DeliveryOrderProcessor
    {
        [FunctionName("DeliveryOrderProcessor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] OrderDetails request,
            [CosmosDB(
                databaseName: "EShopOnWebDb",
                collectionName: "Delivery",
                ConnectionStringSetting = "CosmosDBConnection",
                PartitionKey = "{OrderId}")] IAsyncCollector<OrderDetails> item,
            ILogger log)
        {
            var str = JsonSerializer.Serialize(request);
            log.LogInformation($"C# HTTP trigger function processed a request. Payload {str}");

            try
            {
                await item.AddAsync(request);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                throw;
            }

            return new OkObjectResult("Message processed");
        }
    }
}
