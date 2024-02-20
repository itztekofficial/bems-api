using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;

namespace Admin.Services.Contracts
{
    public interface IWorkFlowService
    {
        Task<WorkFlowCombinedDataResponse> GetCombinedData();
        Task<IEnumerable<WorkFlow>> GetAllAsync();
        Task<WorkFlow> GetByIdAsync(int id);
        Task<bool> CreateAsync(WorkFlow workflow);
        Task<bool> UpdateAsync(WorkFlow workflow);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}