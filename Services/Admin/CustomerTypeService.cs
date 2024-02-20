using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// CustomerTypeService
    /// </summary>
    public class CustomerTypeService : ICustomerTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Entitys
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerType>> GetAllAsync()
        {
            return await _unitOfWork.CustomerTypes.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerType> GetByIdAsync(int id)
        {
            return await _unitOfWork.CustomerTypes.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="CustomerTypes"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(CustomerType entity)
        {
            return await _unitOfWork.CustomerTypes.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="CustomerTypes"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(CustomerType entity)
        {
            return await _unitOfWork.CustomerTypes.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.CustomerTypes.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}