using System;
using Azure.Identity;
using BlazorShared;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Logging;
using Microsoft.eShopWeb.Infrastructure.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.eShopWeb.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<HttpService>();
            services.AddScoped<IOrderItemsReserverService, OrderItemsReserverService>();
            services.AddScoped<IDeliveryOrderProcessorService, DeliveryOrderProcessorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IServiceBusSenderClient, ServiceBusSenderClient>();

            return services;
        }

        public static IServiceCollection AddAzureServices(this IServiceCollection services, BaseUrlConfiguration configuration, ServiceBusOptions serviceBusOptions)
        {
            if (!string.IsNullOrEmpty(configuration.VaultUri))
            {
                services.AddAzureClients(azureClientsBuilder =>
                {
                    azureClientsBuilder.AddSecretClient(new Uri(configuration.VaultUri));
                    azureClientsBuilder.UseCredential(
                        new ChainedTokenCredential(new ManagedIdentityCredential(), new VisualStudioCredential()));
                });
            }
            services.AddAzureClients(azureClientsBuilder =>
                {
                    azureClientsBuilder.AddServiceBusClient(serviceBusOptions.ServiceBusConnectionString);
                });

            return services;
        }
    }
}
