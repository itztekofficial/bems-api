using Core.Models.Request;
using Core.Models.Response;
using Main.Services.Contracts.Company;
using Repositories.Contracts;

namespace Main.Services.Company
{
    /// <summary>
    /// UtilityService
    /// </summary>
    public class UtilityService : IUtilityService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// UtilityService Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UtilityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CommonDropdown>> GetCityByStateId(int stateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommonDropdown>> GetStateByCountryId(int countryId)
        {
            throw new NotImplementedException();
        }

        public Task<MasterResponse> MasterList(UtilityRequest utilityRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// MasterList
        /// </summary>
        /// <returns></returns>
        //public async Task<MasterResponse> MasterList(UtilityRequest utilityRequest)
        //{
        //    try
        //    {
        //        return await _unitOfWork.Utilitys.MasterList(utilityRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog("UtilityService", "MasterList", ex.Message);
        //        throw;
        //    }
        //}

        /// <summary>
        /// GetStateByCountryId
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<CommonDropdown>> GetStateByCountryId(int countryId)
        //{
        //    try
        //    {
        //        return await _unitOfWork.Utilitys.GetStateByCountryId(countryId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog("UtilityService", "GetStateByCountryId", ex.Message);
        //        throw;
        //    }
        //}

        /// <summary>
        /// GetCityByStateId
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<CommonDropdown>> GetCityByStateId(int stateId)
        //{
        //    try
        //    {
        //        return await _unitOfWork.Utilitys.GetCityByStateId(stateId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog("UtilityService", "GetCityByStateId", ex.Message);
        //        throw;
        //    }
        //}
    }
}