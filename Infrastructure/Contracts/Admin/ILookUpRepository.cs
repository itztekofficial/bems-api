using Core.DataModel;
using NPOI.SS.Formula.Functions;

namespace Admin.Repositories.Contracts
{
    public interface ILookUpRepository : IRepository<LookUpModel>
    {
        Task<IEnumerable<LookUpModel>> GetAllByLookTypeIdAsync(int lookTypeId);
        
    }
}