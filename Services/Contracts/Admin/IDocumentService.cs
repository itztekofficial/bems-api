using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document> GetByIdAsync(int id);
        Task<bool> CreateAsync(Document entity);
        Task<bool> UpdateAsync(Document entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}