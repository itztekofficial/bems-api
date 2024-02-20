using Core.Models.Request;
using Core.Models.Response;

namespace Company.Repositories.Contracts;
public interface IRequestApprovalRepository
{
     Task<IEnumerable<ApprovalResponse>> GetApprovalListAsync(ApprovalRequest approvalRequest);
}
