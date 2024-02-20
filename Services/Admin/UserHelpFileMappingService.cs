using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// UserHelpFileMappingService
    /// </summary>
    public class UserHelpFileMappingService : IUserHelpFileMappingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserHelpFileMappingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get UserHelpFileMapping
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserHelpFileMapping>> GetAllAsync()
        {
            return await _unitOfWork.UserHelpFileMappings.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserHelpFileMapping> GetByIdAsync(int id)
        {
            return await _unitOfWork.UserHelpFileMappings.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="UserHelpFileMapping"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(UserHelpFileMapping userHelpFileMapping)
        {
            return await _unitOfWork.UserHelpFileMappings.CreateAsync(userHelpFileMapping);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="UserHelpFileMapping"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UserHelpFileMapping userHelpFileMapping)
        {
            return await _unitOfWork.UserHelpFileMappings.UpdateAsync(userHelpFileMapping);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.UserHelpFileMappings.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}