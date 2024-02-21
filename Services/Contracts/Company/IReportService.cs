using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services.Contracts
{
    public interface IReportService
    {
        Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest);
        Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest);
    }
}