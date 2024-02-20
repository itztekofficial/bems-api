using Company.Repositories.Contracts;
using Company.Services.Contracts;
using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services
{
    /// <summary>
    /// CompanyDashBoardService
    /// </summary>
    public class CompanyDashBoardService : ICompanyDashBoardService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// CompanyDashBoardService
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CompanyDashBoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Company DashBoard Data
        /// </summary>
        /// <returns></returns>
        public async Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request)
        {
            return await _unitOfWork.CompanyDashBoards.GetCompanyDashBoardData(request);
        }

        /// <summary>
        /// Get Request History Data
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId)
        {
            return await _unitOfWork.CompanyDashBoards.GetRequestHistory(initiationId);
        }
    }
}