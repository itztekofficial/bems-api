using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
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