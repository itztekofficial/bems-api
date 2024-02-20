using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface ITermValidityService
    {
        Task<IEnumerable<TermValidity>> GetAllAsync();
        Task<TermValidity> GetByIdAsync(int id);
        Task<bool> CreateAsync(TermValidity termValidity);
        Task<bool> UpdateAsync(TermValidity termValidity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}