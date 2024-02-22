using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(int id);
        Task<bool> CreateAsync(Country country);
        Task<bool> UpdateAsync(Country country);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}