using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<bool> CreateAsync(Department entity);
        Task<bool> UpdateAsync(Department entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}