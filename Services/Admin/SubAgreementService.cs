using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// SubAgreementService
    /// </summary>
    public class SubAgreementService : ISubAgreementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubAgreementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get SubAgreements
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SubAgreement>> GetAllAsync()
        {
            return await _unitOfWork.SubAgreements.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SubAgreement> GetByIdAsync(int id)
        {
            return await _unitOfWork.SubAgreements.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(SubAgreement entity)
        {
            return await _unitOfWork.SubAgreements.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SubAgreement entity)
        {
            return await _unitOfWork.SubAgreements.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.SubAgreements.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}