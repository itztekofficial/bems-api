using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IEntityService
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<bool> CreateAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}