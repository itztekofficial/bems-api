using Company.Repositories;
using Company.Repositories.Contracts;
using Company.Services;
using Company.Services.Contracts;
using Core.Util;
using Login.Repositories.Contracts;
using Login.Repositories;
using Login.Services.Contracts;
using Login.Services;
using Microsoft.Extensions.DependencyInjection;
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
         //   services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUtilityRepository, UtilityRepository>();
            services.AddTransient<IUtilityService, UtilityService>();

            services.AddTransient<IInitiationDetailRepository, InitiationDetailRepository>();
            services.AddTransient<IInitiationDetailService, InitiationDetailService>();

           // services.AddTransient<ICompanyDashBoardRepository, CompanyDashBoardRepository>();
            services.AddTransient<ICompanyDashBoardService, CompanyDashBoardService>();

            services.AddTransient<IRequestApprovalRepository, RequestApprovalRepository>();
            services.AddTransient<IRequestApprovalService, RequestApprovalService>();

            //services.AddTransient<IReportRepository, ReportRepository>();  Commented by Manoj 
            //services.AddTransient<IReportService, ReportService>();

            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();

            services.AddTransient<IUtilRepository, UtilRepository>();
            services.AddTransient<IUtilService, UtilService>();

            return services;
        }
    }
}