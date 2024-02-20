using Core.Models.Request;
using Core.Models.Response;

namespace Company.Repositories.Contracts
{
    public interface IUtilityRepository
    {
        Task<MasterResponse> MasterList(UtilityRequest utilityRequest);

        Task<IEnumerable<CommonDropdown>> GetStateByCountryId(int countryId);

        Task<IEnumerable<CommonDropdown>> GetCityByStateId(int stateId);
    }
}