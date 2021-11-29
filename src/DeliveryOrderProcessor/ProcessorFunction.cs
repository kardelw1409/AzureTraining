using System;
using System.IO;
using System.Threading.Tasks;
using DeliveryOrderProcessor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeliveryOrderProcessor
{
    public class ProcessorFunction
    {
        [FunctionName(nameof(SaveOrderDetails))]
        public async Task<IActionResult> SaveOrderDetails(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "save-order-details")] HttpRequest req,
            [CosmosDB(
                databaseName: "DeliveryOrderDB",
                collectionName: "OrderDetails",
                ConnectionStringSetting = "DeliveryOrderDBConnection")] IAsyncCollector<OrderDetails> toDoItemsOut,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<OrderDetails>(requestBody);

            await toDoItemsOut.AddAsync(data);

            return new CreatedResult("CosmosDb", data);
        }
    }
}
