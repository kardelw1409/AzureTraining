using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class ServiceBusSenderClient : IServiceBusSenderClient
    {
        private ServiceBusOptions _options;
        private ServiceBusSender _sender;

        public ServiceBusSenderClient(IConfiguration configuration, ServiceBusClient senderClient)
        {
            _options = new ServiceBusOptions();
            configuration.Bind(ServiceBusOptions.CONFIG_NAME, _options);
            _sender = senderClient.CreateSender(_options.QueueName);
        }

        public async Task SendMessageAsync<T>(BusMessage<T> message)
        {
            await _sender.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
        }
    }

    public class BusMessage<T>
    {
        public string MessageType { get; set; }
        public T Data { get; set; }
    }
}