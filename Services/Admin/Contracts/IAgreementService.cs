using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IAgreementService
    {
        Task<IEnumerable<Agreement>> GetAllAsync();
        Task<Agreement> GetByIdAsync(int id);
        Task<bool> CreateAsync(Agreement entity);
        Task<bool> UpdateAsync(Agreement entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}