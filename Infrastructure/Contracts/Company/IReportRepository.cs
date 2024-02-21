using Core.Models.Request;
using Core.Models.Response;

namespace Repositories.Contracts.Company;
public interface IReportRepository
{
    //Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest);  Manoj
    Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest);
}
