using Core.Models.Request;
using Core.Models.Response;

namespace Company.Repositories.Contracts;
public interface ICompanyDashBoardRepository
{
    Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request);

    Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId);
}
