using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// PaymentTermService
    /// </summary>
    public class PaymentTermService : IPaymentTermService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentTermService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get PaymentTerms
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentTerm>> GetAllAsync()
        {
            return await _unitOfWork.PaymentTerms.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PaymentTerm> GetByIdAsync(int id)
        {
            return await _unitOfWork.PaymentTerms.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(PaymentTerm entity)
        {
            return await _unitOfWork.PaymentTerms.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(PaymentTerm entity)
        {
            return await _unitOfWork.PaymentTerms.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.PaymentTerms.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}