using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// EntityService
    /// </summary>
    public class EntityService : IEntityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Entitys
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _unitOfWork.Entitys.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Entity> GetByIdAsync(int id)
        {
            return await _unitOfWork.Entitys.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Entity entity)
        {
            return await _unitOfWork.Entitys.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Entity entity)
        {
            return await _unitOfWork.Entitys.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.Entitys.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}