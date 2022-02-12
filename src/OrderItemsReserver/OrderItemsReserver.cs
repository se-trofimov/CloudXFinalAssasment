using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;


namespace OrderItemsReserver
{
    public static class OrderItemsReserver
    {
 
        [FunctionName("OrderItemsReserver")]
        public static async Task Run(
            [ServiceBusTrigger("orderitemsreserver", Connection = "ServiceBusConnection")] OrderItemsMessage message,
            [Blob("orders/{DateTime}.json", FileAccess.Write, Connection = "AzureBlobStorageConnection")] Stream outputBlob,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request. Data Received: {JsonSerializer.Serialize(message)}");
            await JsonSerializer.SerializeAsync(outputBlob, message.Items);
        }
    }
}
