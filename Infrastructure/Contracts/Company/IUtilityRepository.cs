namespace Repositories.Contracts.Company
{
    public interface IUtilityRepository
    {
        Task<MasterResponse> MasterList(UtilityRequest utilityRequest);

        Task<IEnumerable<CommonDropdown>> GetStateByCountryId(int countryId);

        Task<IEnumerable<CommonDropdown>> GetCityByStateId(int stateId);
    }
}