using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface ISubAgreementService
    {
        Task<IEnumerable<SubAgreement>> GetAllAsync();
        Task<SubAgreement> GetByIdAsync(int id);
        Task<bool> CreateAsync(SubAgreement entity);
        Task<bool> UpdateAsync(SubAgreement entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}