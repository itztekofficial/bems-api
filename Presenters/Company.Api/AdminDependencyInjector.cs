
using Admin.Services;
using System.Data.SqlClient;
using System.Data;

namespace Admin.Api
{
    /// <summary>
    /// Add Login Dependency
    /// </summary>
    public static class AdminDependencyInjector
    {
        /// <summary>
        /// Add Admin Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddAdminServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped((s) => new SqlConnection(connectionString));
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ILookUpRepository, LookUpRepository>();
            services.AddTransient<ILookUpService, LookUpService>();

            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IUserRoleService, UserRoleService>();

            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICompanyService, CompanyService>();

            services.AddTransient<IAgreementRepository, AgreementRepository>();
            services.AddTransient<IAgreementService, AgreementService>();

            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDepartmentService, DepartmentService>();

            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IDocumentService, DocumentService>();

            services.AddTransient<IEntityRepository, EntityRepository>();
            services.AddTransient<IEntityService, EntityService>();           

            services.AddTransient<IEntityTypeRepository, EntityTypeRepository>();
            services.AddTransient<IEntityTypeService, EntityTypeService>();

            services.AddTransient<ICustomerTypeRepository, CustomerTypeRepository>();
            services.AddTransient<ICustomerTypeService, CustomerTypeService>();

            services.AddTransient<IPaymentTermRepository, PaymentTermRepository>();
            services.AddTransient<IPaymentTermService, PaymentTermService>();

            services.AddTransient<ISubAgreementRepository, SubAgreementRepository>();
            services.AddTransient<ISubAgreementService, SubAgreementService>();

            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<ISettingsService, SettingsService>();

            services.AddTransient<IActivityLogRepository, ActivityLogRepository>();
            services.AddTransient<IActivityLogService, ActivityLogService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<ITermValidityRepository, TermValidityRepository>();
            services.AddTransient<ITermValidityService, TermValidityService>();

            services.AddTransient<IAdminDashBoardRepository, AdminDashBoardRepository>();
            services.AddTransient<IAdminDashBoardService, AdminDashBoardService>();

            services.AddTransient<IFieldMappingRepository, FieldMappingRepository>();
            services.AddTransient<IFieldMappingService, FieldMappingService>();

            services.AddTransient<IUserHelpFileMappingRepository, UserHelpFileMappingRepository>();
            services.AddTransient<IUserHelpFileMappingService, UserHelpFileMappingService>();

            services.AddTransient<IContractTemplateRepository, ContractTemplateRepository>();
            services.AddTransient<IContractTemplateService, ContractTemplateService>();

            services.AddTransient<IRepositoryTemplateRepository, RepositoryTemplateRepository>();
            services.AddTransient<IRepositoryTemplateService, RepositoryTemplateService>();

            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryService, CountryService>();

            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IStateService, StateService>();

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICityService, CityService>();

            services.AddTransient<IWorkFlowRepository, WorkFlowRepository>();
            services.AddTransient<IWorkFlowService, WorkFlowService>();

            services.AddTransient<IEmailSetupRepository, EmailSetupRepository>();
            services.AddTransient<IEmailSetupService, EmailSetupService>();

            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();

        

            return services;
        }
    }
}