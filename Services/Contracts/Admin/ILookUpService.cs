using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Contracts.Admin
{
    public interface ILookUpService
    {
        Task<List<LookUpResponse>> GetAllAsync();
        Task<List<LookUpResponse>> GetAllByLookTypeIdAsync(int lookTypeId);
        Task<LookUpModel> GetByIdAsync(int id);
        Task<bool> CreateAsync(LookUpModel entity);
        Task<bool> UpdateAsync(LookUpModel entity);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}