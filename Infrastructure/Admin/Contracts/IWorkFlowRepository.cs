using Core.DataModel;
using Core.Models.Response;

namespace Admin.Repositories.Contracts;
public interface IWorkFlowRepository : IRepository<WorkFlow>
{
    Task<WorkFlowCombinedDataResponse> GetCombinedData();
}
