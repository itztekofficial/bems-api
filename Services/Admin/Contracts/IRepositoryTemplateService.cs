using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IRepositoryTemplateService
    {
        Task<IEnumerable<RepositoryTemplate>> GetAllAsync();
        Task<RepositoryTemplate> GetByIdAsync(int id);
        Task<bool> CreateAsync(RepositoryTemplate repositoryTemplate);
        Task<bool> UpdateAsync(RepositoryTemplate repositoryTemplate);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}