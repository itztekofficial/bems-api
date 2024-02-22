using Core.DataModel;

namespace Repositories.Contracts.Admin
{
    public interface ILookUpRepository : IRepository<LookUpModel>
    {
        Task<IEnumerable<LookUpModel>> GetAllByLookTypeIdAsync(int lookTypeId);

    }
}