using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Contracts.Admin
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