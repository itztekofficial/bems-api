using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface ICustomerTypeService
    {
        Task<IEnumerable<CustomerType>> GetAllAsync();
        Task<CustomerType> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerType entity);
        Task<bool> UpdateAsync(CustomerType entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}