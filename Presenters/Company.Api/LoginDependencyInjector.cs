using Login.Repositories;
using Main.Services.Contracts.Login;
using Main.Services.Login;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Contracts.Login;
using System.Data;
using System.Data.SqlClient;

namespace Login.Api
{
    /// <summary>
    /// Add Login Dependency
    /// </summary>
    public static class LoginDependencyInjector
    {
        /// <summary>
        /// Add Login Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoginServices(this IServiceCollection services, string connectionString)
        {
            //services.AddTransient<DBUp.Contracts.IDatabaseMigration, DBUp.DatabaseMigration>();

            services.AddScoped((s) => new SqlConnection(connectionString));
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();

            //services.AddTransient<IUtilRepository, UtilRepository>(); Manoj
            //services.AddTransient<IUtilService, UtilService>();
            return services;
        }
    }
}