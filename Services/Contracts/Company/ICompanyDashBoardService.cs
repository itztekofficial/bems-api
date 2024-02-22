using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Contracts.Company
{
    public interface ICompanyDashBoardService
    {
        Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request);

        Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId);
    }
}