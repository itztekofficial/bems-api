namespace Company.Repositories.Contracts;
public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();

    ICompanyDashBoardRepository CompanyDashBoards { get; }
    IInitiationDetailRepository InitiationDetails { get; }
    IRequestApprovalRepository RequestApprovals { get; }
    IUtilityRepository Utilitys { get; }
    IReportRepository Reports { get; }
    INotificationRepository Notifications { get; }
}