using Core.DataModel;
//using NPOI.SS.Formula.Functions; Manoj

namespace Repositories.Contracts.Admin
{
    public interface ILookUpRepository : IRepository<LookUpModel>
    {
        Task<IEnumerable<LookUpModel>> GetAllByLookTypeIdAsync(int lookTypeId);
        
    }
}