using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
{
    public interface IEmailSetupService
    {
        Task<IEnumerable<EmailSetup>> GetAllAsync();
        Task<EmailSetup> GetByIdAsync(int id);
        Task<bool> CreateAsync(EmailSetup emailSetup);
        Task<bool> UpdateAsync(EmailSetup emailSetup);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}