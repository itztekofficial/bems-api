using System.Data;
using System.Data.SqlClient;
using Core.AppConst;
using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Dapper;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Company;

namespace Repositories.Company
{
    public class UtilityRepository : IUtilityRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<UtilityRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public UtilityRepository(ILogger<UtilityRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.Log(LogLevel.Information, "UtilityRepository => Initialized");
        }
        #endregion

        #region ===[ UtilityRepository Methods ]===========================
        /// <summary>
        /// MasterList
        /// </summary>
        /// <returns></returns>
        public async Task<MasterResponse> MasterList(UtilityRequest utilityRequest)
        {
            try
            {
                MasterResponse response = new();
                var param = new DynamicParameters();
                param.Add("ActionType", "getAllMasters");
                param.Add("entityId", utilityRequest.EntityId);
                param.Add("departmentId", utilityRequest.DepartmentId);

                var cmd = new CommandDefinition(AppConst.usp_Utility, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
                var res = await _sqlConnection.QueryMultipleAsync(cmd);

                response.CountryList = res.Read<CommonDropdown>();
                //response.StateList = res.Read<CommonDropdown>();
                response.EntityList = res.Read<CommonDropdown>();
                response.EntityTypeList = res.Read<CommonDropdown>();
                response.CustomerTypeList = res.Read<CommonDropdown>();
                response.AgreementList = res.Read<CommonDropdown>();
                response.PaymentTermList = res.Read<CommonDropdown>();

                //=====For Legal====
                response.AgreementSubCategoryList = res.Read<CommonDropdown>();
                response.LegalUserList = res.Read<CommonDropdown>();
                response.DocumentList = res.Read<DocumentDropdown>();
                response.ProductList = res.Read<CommonDropdown>();
                response.TermValidityList = res.Read<CommonDropdown>();
                response.DocShareModeList = res.Read<CommonDropdown>();
                response.RoleTypeList = res.Read<CommonDropdown>();
                response.ConfirmationOfHardCopy = res.Read<CommonDropdown>();

                return response;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilityRepository", "CountryList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetStateByCountryId
        /// </summary>
        /// <param name="stateId">int</param>
        /// <returns></returns>
        public async Task<IEnumerable<CommonDropdown>> GetStateByCountryId(int countryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getStateByCountryId");
                param.Add("Id", countryId);

                var cmd = new CommandDefinition(AppConst.usp_Utility, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
                return await _sqlConnection.QueryAsync<CommonDropdown>(cmd);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilityRepository", "GetStateByCountryId", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetCityByStateId
        /// </summary>
        /// <param name="stateId">int</param>
        /// <returns></returns>
        public async Task<IEnumerable<CommonDropdown>> GetCityByStateId(int stateId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getCityByStateId");
                param.Add("Id", stateId);

                var cmd = new CommandDefinition(AppConst.usp_Utility, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
                return await _sqlConnection.QueryAsync<CommonDropdown>(cmd);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilityRepository", "GetCityByStateId", ex.Message);
                throw;
            }
        }
        #endregion
    }
}