using Core.Models.Request;
using Core.Models.Response;
using Main.Services.Contracts.Company;
using Repositories.Contracts;

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

        public Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest)
        //{
        //    return await _unitOfWork.Reports.GetAllContractsAsync(reportRequest);
        //}

        //public async Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest)
        //{
        //    return await _unitOfWork.Reports.GetExportAllContractsAsync(reportRequest);
        //}
    }
}