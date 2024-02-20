using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IEntityTypeService
    {
        Task<IEnumerable<EntityType>> GetAllAsync();
        Task<EntityType> GetByIdAsync(int id);
        Task<bool> CreateAsync(EntityType entitytype);
        Task<bool> UpdateAsync(EntityType entitytype);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}