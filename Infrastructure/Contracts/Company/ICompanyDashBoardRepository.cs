namespace Repositories.Contracts.Company;
public interface ICompanyDashBoardRepository
{
    Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request);

    Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId);
}
