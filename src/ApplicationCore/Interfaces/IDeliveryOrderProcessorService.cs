using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IDeliveryOrderProcessorService
    {
        Task SaveOrderDetails(object orderDetails);
    }
}