using Core.DataModel;

namespace Admin.Services.Contracts
{
    public interface IActivityLogService
    {
        Task<IEnumerable<ActivityLog>> GetAllAsync();
        Task<ActivityLog> GetByIdAsync(int id);
        Task<bool> CreateAsync(ActivityLog activityLog);
        Task<bool> UpdateAsync(ActivityLog activityLog);
        Task<bool> DeleteAsync(int id);
    }
}