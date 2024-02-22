using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
{
    public interface IEmailTemplateService
    {
        Task<IEnumerable<EmailTemplate>> GetAllAsync();
        Task<EmailTemplate> GetByIdAsync(int id);
        Task<bool> CreateAsync(EmailTemplate emailTemplate);
        Task<bool> UpdateAsync(EmailTemplate emailTemplate);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}