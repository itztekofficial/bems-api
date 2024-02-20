using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// AgreementService
    /// </summary>
    public class AgreementService : IAgreementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgreementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Agreements
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Agreement>> GetAllAsync()
        {
            return await _unitOfWork.Agreements.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Agreement> GetByIdAsync(int id)
        {
            return await _unitOfWork.Agreements.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Agreement entity)
        {
            return await _unitOfWork.Agreements.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Agreement entity)
        {
            return await _unitOfWork.Agreements.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.Agreements.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}