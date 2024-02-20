using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;

namespace Admin.Services
{
    /// <summary>
    /// WorkFlowService
    /// </summary>
    public class WorkFlowService : IWorkFlowService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkFlowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get CombinedData
        /// </summary>
        /// <returns></returns>
        public async Task<WorkFlowCombinedDataResponse> GetCombinedData()
        {
            return await _unitOfWork.WorkFlows.GetCombinedData();
        }

        /// <summary>
        /// Get Departments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WorkFlow>> GetAllAsync()
        {
            return await _unitOfWork.WorkFlows.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WorkFlow> GetByIdAsync(int id)
        {
            return await _unitOfWork.WorkFlows.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(WorkFlow workflow)
        {
            return await _unitOfWork.WorkFlows.CreateAsync(workflow);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(WorkFlow workflow)
        {
            return await _unitOfWork.WorkFlows.UpdateAsync(workflow);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.WorkFlows.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}