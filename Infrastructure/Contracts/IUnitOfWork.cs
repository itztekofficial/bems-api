namespace Admin.Repositories.Contracts;
public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();

    ILookUpRepository LookUps { get; }
    IDepartmentRepository Departments { get; }
    IDocumentRepository Documents { get; }
    IEntityRepository Entitys { get; }
    IEntityTypeRepository EntitysType { get; }
    ICustomerTypeRepository CustomerTypes { get; }
    IPaymentTermRepository PaymentTerms { get; }
    IAgreementRepository Agreements { get; }
    ISubAgreementRepository SubAgreements { get; }
    IActivityLogRepository ActivityLogs { get; }
    IProductRepository Products { get; }
    ITermValidityRepository TermValiditys { get; }    
    IFieldMappingRepository FieldMappings { get; }
    IUserHelpFileMappingRepository UserHelpFileMappings { get; }
    IContractTemplateRepository ContractTemplates { get; }
    IRepositoryTemplateRepository RepositoryTemplates { get; }
    ICountryRepository Countries { get; }
    IStateRepository States { get; }
    ICityRepository Citys { get; }
    IWorkFlowRepository WorkFlows { get; }
    IEmailSetupRepository EmailSetups { get; }
    IEmailTemplateRepository EmailTemplates { get; }
}