using Login.Repositories;
using Login.Repositories.Contracts;
using Login.Services;
using Login.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddTransient<IUtilRepository, UtilRepository>();
            services.AddTransient<IUtilService, UtilService>();
            return services;
        }
    }
}