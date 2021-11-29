using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OrderItemsReserver
{
    public class DeadLetterQueueTriggerFunction
    {
        private readonly IConfiguration _configuration;

        public DeadLetterQueueTriggerFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("DeadLetterQueueTriggerFunction")]
        public async Task TriggerDeadLetterQueue(
            [ServiceBusTrigger("order/$DeadLetterQueue")] byte[] queueItem,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var deadLetterQueueItem = Encoding.UTF8.GetString(queueItem);

            var httpClient = new HttpClient();

            await httpClient.PostAsync(
                _configuration?.GetValue<string>("LogicAppEmailSenderUri"),
                new StringContent(deadLetterQueueItem, Encoding.UTF8, "application/json"));
        }
    }
}