using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Services;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IServiceBusSenderClient
    {
        Task SendMessageAsync<T>(BusMessage<T> message);
    }
}