using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services.Contracts
{
    public interface IRequestApprovalService
    {
        Task<IEnumerable<ApprovalResponse>> GetApprovalListAsync(ApprovalRequest approvalRequest);
    }
}