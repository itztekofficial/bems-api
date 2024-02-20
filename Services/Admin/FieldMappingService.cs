using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// EntityTypeService
    /// </summary>
    public class FieldMappingService : IFieldMappingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FieldMappingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Entitys
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FieldMapping>> GetAllAsync()
        {
            return await _unitOfWork.FieldMappings.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FieldMapping> GetByIdAsync(int id)
        {
            return await _unitOfWork.FieldMappings.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(FieldMapping fieldMapping)
        {
            return await _unitOfWork.FieldMappings.CreateAsync(fieldMapping);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(FieldMapping fieldMapping)
        {
            return await _unitOfWork.FieldMappings.UpdateAsync(fieldMapping);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.FieldMappings.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}