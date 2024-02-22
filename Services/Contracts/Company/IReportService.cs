using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Contracts.Company
{
    public interface IReportService
    {
        //    Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest); Manoj
        Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest);
    }
}