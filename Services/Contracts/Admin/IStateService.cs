using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services.Contracts
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetAllAsync();
        Task<State> GetByIdAsync(int id);
        Task<bool> CreateAsync(State state);
        Task<bool> UpdateAsync(State state);
        Task<bool> DeleteAsync(DeleteRequest request);
    }
}