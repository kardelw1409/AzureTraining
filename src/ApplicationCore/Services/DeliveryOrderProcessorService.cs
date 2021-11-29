using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorShared;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class DeliveryOrderProcessorService : IDeliveryOrderProcessorService
    {
        private readonly ILogger<OrderItemsReserverService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;


        public DeliveryOrderProcessorService(
            BaseUrlConfiguration baseUrlConfiguration,
            ILogger<OrderItemsReserverService> logger)
        {
            _apiUrl = baseUrlConfiguration.OrderProcessorBase;
            _httpClient = new HttpClient { BaseAddress = new Uri(_apiUrl) };
            _logger = logger;
        }

        public async Task SaveOrderDetails(object orderDetails)
        {
            var content = ToJson(orderDetails);

            var result = await _httpClient.PostAsync($"{_apiUrl}save-order-details", content);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }

        private StringContent ToJson(object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }
    }
}