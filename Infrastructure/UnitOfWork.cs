using System.Data;
using Repositories.Contracts;
using Repositories.Contracts.Admin;

namespace Infrastructure.Repositories;    //.Repository
public class UnitOfWork : IUnitOfWork, IDisposable
{
    readonly IDbTransaction _dbTransaction;
    public ILookUpRepository LookUps { get; }
    public IDepartmentRepository Departments { get; }
    public IDocumentRepository Documents { get; }
    //public IEntityRepository Entitys { get; }
    //public ICustomerTypeRepository CustomerTypes { get; }
    //public IEntityTypeRepository EntitysType { get; }
    //public IPaymentTermRepository PaymentTerms { get; }
    //public IAgreementRepository Agreements { get; }
    //public ISubAgreementRepository SubAgreements { get; }
    //public IActivityLogRepository ActivityLogs { get; }
    //public IProductRepository Products { get; }
    //public ITermValidityRepository TermValiditys { get; }
    //public IFieldMappingRepository FieldMappings { get; }
    //public IUserHelpFileMappingRepository UserHelpFileMappings { get; }
    //public IContractTemplateRepository ContractTemplates { get; }
    //public IRepositoryTemplateRepository RepositoryTemplates { get; }
    public ICountryRepository Countries { get; }
    public IStateRepository States { get; }
    public ICityRepository Citys { get; }
    //public IWorkFlowRepository WorkFlows { get; } Manoj
    public IEmailSetupRepository EmailSetups { get; }
    // public IEmailTemplateRepository EmailTemplates { get; } Manoj

    public UnitOfWork(IDbTransaction dbTransaction,
        ILookUpRepository _lookUps,
        IDepartmentRepository _departments,
        IDocumentRepository _documents,
        //IEntityRepository _entitys, Manoj
        //IEntityTypeRepository _entitysType,
        //ICustomerTypeRepository _customerType,
        //IPaymentTermRepository _paymentTerms,
        //IAgreementRepository _agreements,
        //ISubAgreementRepository _subAgreements,
        //IActivityLogRepository _activityLogs,
        //IProductRepository _products,
        //ITermValidityRepository _termValiditys,
        //IFieldMappingRepository _fieldMappings,
        //IUserHelpFileMappingRepository _userHelpFileMappings,
        //IContractTemplateRepository _contractTemplates,
        //IRepositoryTemplateRepository _repositoryTemplates,
        ICountryRepository _countries,
        IStateRepository _states,
        ICityRepository _citys,
        // IWorkFlowRepository _workflows, Manoj
        IEmailSetupRepository _emailSetups
        // IEmailTemplateRepository _emailTemplates  Manoj
        )
    {
        _dbTransaction = dbTransaction;
        LookUps = _lookUps;
        Departments = _departments;
        Documents = _documents;
        //Entitys = _entitys;
        //EntitysType = _entitysType;
        //CustomerTypes = _customerType;
        //PaymentTerms = _paymentTerms;
        //Agreements = _agreements;
        //SubAgreements = _subAgreements;
        //ActivityLogs = _activityLogs;
        //Products = _products;
        //TermValiditys = _termValiditys;
        //FieldMappings = _fieldMappings;
        //UserHelpFileMappings = _userHelpFileMappings;
        //ContractTemplates = _contractTemplates;
        //RepositoryTemplates = _repositoryTemplates;
        Countries = _countries;
        States = _states;
        Citys = _citys;
        //WorkFlows = _workflows; Manoj
        EmailSetups = _emailSetups;
        //EmailTemplates = _emailTemplates;  Manoj
    }

    public void Commit()
    {
        try
        {
            _dbTransaction.Commit();
        }
        catch
        {
            _dbTransaction.Rollback();
        }
    }

    public void Rollback()
    {
        _dbTransaction.Rollback();
    }

    //Close the SQL Connection and dispose the objects
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbTransaction.Connection?.Close();
                _dbTransaction.Connection?.Dispose();
                _dbTransaction.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}