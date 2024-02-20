using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IPaymentTermService
    {
        Task<IEnumerable<PaymentTerm>> GetAllAsync();
        Task<PaymentTerm> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentTerm entity);
        Task<bool> UpdateAsync(PaymentTerm entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}