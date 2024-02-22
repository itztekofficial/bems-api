using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Company
{
    /// <summary>
    /// Report Service
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ReportService
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest)
        {
            return await _unitOfWork.Reports.GetAllContractsAsync(reportRequest);
        }

        public async Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest)
        {
            return await _unitOfWork.Reports.GetExportAllContractsAsync(reportRequest);
        }
    }
}