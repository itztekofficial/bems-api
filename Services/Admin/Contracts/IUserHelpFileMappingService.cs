using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IUserHelpFileMappingService
    {
        Task<IEnumerable<UserHelpFileMapping>> GetAllAsync();
        Task<UserHelpFileMapping> GetByIdAsync(int id);
        Task<bool> CreateAsync(UserHelpFileMapping userHelpFileMapping);
        Task<bool> UpdateAsync(UserHelpFileMapping userHelpFileMapping);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}