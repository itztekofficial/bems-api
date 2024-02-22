using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task<bool> CreateAsync(City city);
        Task<bool> UpdateAsync(City city);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}