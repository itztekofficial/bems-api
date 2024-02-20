using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IContractTemplateService
    {
        Task<IEnumerable<ContractTemplate>> GetAllAsync();
        Task<ContractTemplate> GetByIdAsync(int id);
        Task<bool> CreateAsync(ContractTemplate contractTemplate);
        Task<bool> UpdateAsync(ContractTemplate contractTemplate);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}