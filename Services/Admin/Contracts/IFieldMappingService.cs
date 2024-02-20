using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IFieldMappingService
    {
        Task<IEnumerable<FieldMapping>> GetAllAsync();
        Task<FieldMapping> GetByIdAsync(int id);
        Task<bool> CreateAsync(FieldMapping fieldMapping);
        Task<bool> UpdateAsync(FieldMapping fieldMapping);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}