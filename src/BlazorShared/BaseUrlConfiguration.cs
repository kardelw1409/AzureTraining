namespace BlazorShared
{
    public class BaseUrlConfiguration
    {
        public const string CONFIG_NAME = "baseUrls";

        public string ApiBase { get; set; }
        public string WebBase { get; set; }
        public string ReserverBase { get; set; }
        public string OrderProcessorBase { get; set; }
        public string VaultUri { get; set; }
        public string ServiceBusConnectionString { get; set; }
    }
}
