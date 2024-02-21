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

            ConnectionStrings connectionStrings = new();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            services.AddSingleton(connectionStrings);
            ConfigFile.SetDBConnectionConfigration(connectionStrings);

            return services;
        }
    }
}