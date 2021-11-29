using System;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.Security.KeyVault.Secrets;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class SecretClientService : ISecretClientService
    {
        private readonly ILogger<SecretClientService> logger;
        private readonly SecretClient secretClient;

        public SecretClientService(SecretClient secretClient, ILogger<SecretClientService> logger)
        {
            this.secretClient = secretClient;
            this.logger = logger;
        }

        public string GetSecretAsync(string secretKey)
        {
            try
            {
                var secret = this.secretClient.GetSecret(secretKey);
                var keyVaultSecret = secret.Value;

                return keyVaultSecret.Value;
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.NotFound)
            {
                this.logger?.LogError(ex, $"Key vault secret '{secretKey}' not found.");
                throw new Exception(secretKey);
            }
        }
    }
}
