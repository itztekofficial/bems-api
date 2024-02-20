using Company.Repositories.Contracts;
using Company.Services.Contracts;
using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services
{
    /// <summary>
    /// RequestApprovalService
    /// </summary>
    public class RequestApprovalService : IRequestApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// RequestApprovalService
        /// </summary>
        /// <param name="requestApprovalRepository"></param>
        public RequestApprovalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GetApprovalList
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApprovalResponse>> GetApprovalListAsync(ApprovalRequest approvalRequest)
        {
            return await _unitOfWork.RequestApprovals.GetApprovalListAsync(approvalRequest);
        }

    }
}