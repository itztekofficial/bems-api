using Core.Models.Request;
using Core.Models.Response;

namespace Company.Repositories.Contracts;
public interface IReportRepository
{
    Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest);
    Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest);
}
