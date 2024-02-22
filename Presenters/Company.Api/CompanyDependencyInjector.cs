using ARepositories.Contracts.Admin;
using Core.Util;
using Infrastructure.Repositories;
using Login.Repositories;
using Main.Services.Admin;
using Main.Services.Company;
using Main.Services.Contracts.Admin;
using Main.Services.Contracts.Company;
using Main.Services.Contracts.Login;
using Main.Services.Login;
using Microsoft.Extensions.DependencyInjection;
using NPOI.SS.Util;
using Repositories.Admin;
using Repositories.Company;
using Repositories.Contracts;
using Repositories.Contracts.Admin;
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

// Login
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();
            //#endregion
            //Admin
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ILookUpRepository, LookUpRepository>();
            services.AddTransient<ILookUpService, LookUpService>();

            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IUserRoleService, UserRoleService>();

            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICompanyService, CompanyService>();

            //services.AddTransient<IAgreementRepository, AgreementRepository>(); Manoj
            //services.AddTransient<IAgreementService, AgreementService>();

            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDepartmentService, DepartmentService>();

            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IDocumentService, DocumentService>();

            //services.AddTransient<IEntityRepository, EntityRepository>(); Manoj
            //services.AddTransient<IEntityService, EntityService>();           

            //services.AddTransient<IEntityTypeRepository, EntityTypeRepository>();
            //services.AddTransient<IEntityTypeService, EntityTypeService>();

            //services.AddTransient<ICustomerTypeRepository, CustomerTypeRepository>();
            //services.AddTransient<ICustomerTypeService, CustomerTypeService>();

            //services.AddTransient<IPaymentTermRepository, PaymentTermRepository>();
            //services.AddTransient<IPaymentTermService, PaymentTermService>();

            //services.AddTransient<ISubAgreementRepository, SubAgreementRepository>();
            //services.AddTransient<ISubAgreementService, SubAgreementService>();

            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<ISettingsService, SettingsService>();

            //services.AddTransient<IActivityLogRepository, ActivityLogRepository>(); Manoj
            //services.AddTransient<IActivityLogService, ActivityLogService>();

            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IProductService, ProductService>();

            //services.AddTransient<ITermValidityRepository, TermValidityRepository>();
            //services.AddTransient<ITermValidityService, TermValidityService>();

            services.AddTransient<IAdminDashBoardRepository, AdminDashBoardRepository>();
            services.AddTransient<IAdminDashBoardService, AdminDashBoardService>();

            //services.AddTransient<IFieldMappingRepository, FieldMappingRepository>(); Manoj
            //services.AddTransient<IFieldMappingService, FieldMappingService>();

            //services.AddTransient<IUserHelpFileMappingRepository, UserHelpFileMappingRepository>();
            //services.AddTransient<IUserHelpFileMappingService, UserHelpFileMappingService>();

            //services.AddTransient<IContractTemplateRepository, ContractTemplateRepository>();
            //services.AddTransient<IContractTemplateService, ContractTemplateService>();

            //services.AddTransient<IRepositoryTemplateRepository, RepositoryTemplateRepository>();
            //services.AddTransient<IRepositoryTemplateService, RepositoryTemplateService>();

            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryService, CountryService>();

            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IStateService, StateService>();

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICityService, CityService>();

            //services.AddTransient<IWorkFlowRepository, WorkFlowRepository>(); Manoj
            //services.AddTransient<IWorkFlowService, WorkFlowService>();

            services.AddTransient<IEmailSetupRepository, EmailSetupRepository>();
            services.AddTransient<IEmailSetupService, EmailSetupService>();

            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            //Admin

            return services;
        }
    }
}