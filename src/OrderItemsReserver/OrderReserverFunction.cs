using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderItemsReserver.Models;

namespace OrderItemsReserver
{
    public static class OrderReserverFunction
    {
        [FunctionName("OrderReserverFunction")]
        public static async Task ReserveOrder(
            [ServiceBusTrigger("Order")] byte[] queueItem,
            [Blob("order-reserver", FileAccess.Write, Connection = "StorageAccountConnectionString")] CloudBlobContainer outputBlobContainer,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var data = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(queueItem));

            var reserveName = $"Order_{Guid.NewGuid()}";

            await outputBlobContainer.CreateIfNotExistsAsync();
            var blobReference = outputBlobContainer.GetBlockBlobReference(reserveName);
            await blobReference.UploadTextAsync(JsonConvert.SerializeObject(data.Data));
        }
    }
}
