using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services.Contracts
{
    public interface IUtilityService
    {
        Task<MasterResponse> MasterList(UtilityRequest utilityRequest);
        
        Task<IEnumerable<CommonDropdown>> GetStateByCountryId(int countryId);

        Task<IEnumerable<CommonDropdown>> GetCityByStateId(int stateId);
    }
}