using System.Threading.Tasks;
using BlazorShared;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class OrderItemsReserverService : IOrderItemsReserverService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<OrderItemsReserverService> _logger;

        public OrderItemsReserverService(HttpService httpService,
            ILogger<OrderItemsReserverService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task Create(Order catalogItem)
        {
            await _httpService.HttpPost<object>("reserve-order", catalogItem);
        }
    }
}