using Core.Models;
using Core.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CoreServiceCollection
    {
        public static IServiceCollection AddConnectCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appSettings = new();
            configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);
            ConfigFile.SetAppConfigration(appSettings);

            StorageSettings storageSettings = new();
            configuration.GetSection("StorageSettings").Bind(storageSettings);
            services.AddSingleton(storageSettings);
            ConfigFile.SetStorageConfigration(storageSettings);

            BusSettings busSettings = new();
            configuration.GetSection("BusSettings").Bind(busSettings);
            services.AddSingleton(busSettings);
            ConfigFile.SetBusConfigration(busSettings);

            ConnectionStrings connectionStrings = new();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            services.AddSingleton(connectionStrings);
            ConfigFile.SetDBConnectionConfigration(connectionStrings);

            ElasticConfiguration elasticConfiguration = new();
            configuration.GetSection("ElasticConfiguration").Bind(elasticConfiguration);
            services.AddSingleton(elasticConfiguration);
            ConfigFile.SetElasticConfigration(elasticConfiguration);

            return services;
        }
    }
}