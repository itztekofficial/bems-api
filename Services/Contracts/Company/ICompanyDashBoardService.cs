using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services.Contracts
{
    public interface ICompanyDashBoardService
    {
        Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request);

        Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId);
    }
}