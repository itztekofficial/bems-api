using Company.Repositories.Contracts;
using System.Data;

namespace Company.Repositories;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    readonly IDbTransaction _dbTransaction;
    public ICompanyDashBoardRepository CompanyDashBoards { get; }
    public IInitiationDetailRepository InitiationDetails { get; }
    public IRequestApprovalRepository RequestApprovals { get; }
    public IUtilityRepository Utilitys { get; }
    public IReportRepository Reports { get; }
    public INotificationRepository Notifications { get; }

    public UnitOfWork(IDbTransaction dbTransaction,
        ICompanyDashBoardRepository _companyDashBoards,
        IInitiationDetailRepository _initiationDetails,
        IRequestApprovalRepository _requestApprovals,
        IUtilityRepository _utilitys,
        IReportRepository _reports,
        INotificationRepository _notifications)
    {
        _dbTransaction = dbTransaction;
        CompanyDashBoards = _companyDashBoards;
        InitiationDetails = _initiationDetails;
        RequestApprovals = _requestApprovals;
        Utilitys = _utilitys;
        Reports = _reports;
        Notifications = _notifications;
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