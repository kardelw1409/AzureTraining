namespace Microsoft.eShopWeb
{
    public class ServiceBusOptions
    {
        public const string CONFIG_NAME = "serviceBusOptions";

        public string ServiceBusConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}