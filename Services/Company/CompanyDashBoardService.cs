using Core.Models.Request;
using Core.Models.Response;
using Main.Services.Contracts.Company;
using Repositories.Contracts;

namespace Main.Services.Company
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

        public Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Company DashBoard Data
        /// </summary>
        /// <returns></returns>
        //public async Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request)
        //{
        //    return await _unitOfWork.CompanyDashBoards.GetCompanyDashBoardData(request);
        //}

        /// <summary>
        /// Get Request History Data
        /// </summary>
        /// <returns></returns>
        //public async Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId)
        //{
        //    return await _unitOfWork.CompanyDashBoards.GetRequestHistory(initiationId);
        //}
    }
}