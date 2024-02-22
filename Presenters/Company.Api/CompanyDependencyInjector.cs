using Core.Util;
using Infrastructure.Repositories;
using Login.Repositories;
using Main.Services.Company;
using Main.Services.Contracts.Company;
using Main.Services.Contracts.Login;
using Main.Services.Login;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Company;
using Repositories.Contracts;
using Repositories.Contracts.Company;
using Repositories.Contracts.Login;
using System.Data;
using System.Data.SqlClient;

namespace Company.Api
{
    /// <summary>
    /// Add Company Dependency
    /// </summary>
    public static class CompanyDependencyInjector
    {
        /// <summary>
        /// Add Company Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddCompanyServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped((s) => new SqlConnection(connectionString));
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });

            services.AddTransient<IMemoryStreamManager, MemoryStreamManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUtilityRepository, UtilityRepository>();
            services.AddTransient<IUtilityService, UtilityService>();

            //services.AddTransient<IInitiationDetailRepository, InitiationDetailRepository>(); Manoj
            //services.AddTransient<IInitiationDetailService, InitiationDetailService>();

            services.AddTransient<ICompanyDashBoardRepository, CompanyDashBoardRepository>();
            services.AddTransient<ICompanyDashBoardService, CompanyDashBoardService>();

            //services.AddTransient<IRequestApprovalRepository, RequestApprovalRepository>();
            //services.AddTransient<IRequestApprovalService, RequestApprovalService>();

            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IReportService, ReportService>();

            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();

            //services.AddTransient<IUtilRepository, UtilRepository>();
            //services.AddTransient<IUtilService, UtilService>();

            return services;
        }
    }
}