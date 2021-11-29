using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface ISecretClientService
    {
        string GetSecretAsync(string secretKey);
    }
}
