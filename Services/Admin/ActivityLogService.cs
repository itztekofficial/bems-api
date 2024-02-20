using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;

namespace Admin.Services
{
    /// <summary>
    /// ActivityLogService
    /// </summary>
    public class ActivityLogService : IActivityLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get ActivityLogs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ActivityLog>> GetAllAsync()
        {
            return await _unitOfWork.ActivityLogs.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActivityLog> GetByIdAsync(int id)
        {
            return await _unitOfWork.ActivityLogs.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ActivityLog activity)
        {
            return await _unitOfWork.ActivityLogs.CreateAsync(activity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ActivityLog activity)
        {
            return await _unitOfWork.ActivityLogs.UpdateAsync(activity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            return await _unitOfWork.ActivityLogs.DeleteAsync(id, false, 1);
        }
    }
}