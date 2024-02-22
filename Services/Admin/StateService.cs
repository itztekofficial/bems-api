using Core.DataModel;
using Core.Models.Request;
using Main.Services.Contracts.Admin;
using Repositories.Contracts;

namespace Main.Services.Admin
{
    /// <summary>
    /// StateService
    /// </summary>
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Departments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await _unitOfWork.States.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<State> GetByIdAsync(int id)
        {
            return await _unitOfWork.States.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(State state)
        {
            return await _unitOfWork.States.CreateAsync(state);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(State state)
        {
            return await _unitOfWork.States.UpdateAsync(state);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.States.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}