using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// EntityTypeService
    /// </summary>
    public class EntityTypeService : IEntityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntityTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Entitys
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EntityType>> GetAllAsync()
        {
            return await _unitOfWork.EntitysType.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EntityType> GetByIdAsync(int id)
        {
            return await _unitOfWork.EntitysType.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(EntityType entityType)
        {
            return await _unitOfWork.EntitysType.CreateAsync(entityType);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(EntityType entityType)
        {
            return await _unitOfWork.EntitysType.UpdateAsync(entityType);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.EntitysType.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}