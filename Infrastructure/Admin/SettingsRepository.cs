using System.Data;
using System.Data.SqlClient;
using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Dapper;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Admin;

namespace Repositories.Admin
{
    public class SettingsRepository : ISettingsRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<UserRoleRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public SettingsRepository(ILogger<UserRoleRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.Log(LogLevel.Information, "SettingsRepository => Initialized");
        }
        #endregion

        #region ===[ Settings Methods ]===========================

        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <returns></returns>
        public async Task<MyProfileResponse> GetUserDetailsById(int uId)
        {
            try
            {
                UserMappingResponse userMappingResponse = new();
                var param = new DynamicParameters();
                param.Add("ActionType", "getUserDetailsById");
                param.Add("Id", uId);

                return await _sqlConnection.QueryFirstOrDefaultAsync<MyProfileResponse>("usp_Settings", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserDetailsById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserPWDById
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserPWDById(int uId)
        {
            try
            {
                UserMappingResponse userMappingResponse = new();
                var param = new DynamicParameters();
                param.Add("ActionType", "getUserPWDById");
                param.Add("Id", uId);

                var usrPass = await _sqlConnection.QueryFirstOrDefaultAsync<string>("usp_Settings", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                return !string.IsNullOrEmpty(usrPass) ? EncryptDecrypt.Decrypt(usrPass) : string.Empty;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserPWDById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserPWDById
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateUserPWDById(PWDChangeRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "updateUserPWDById");
                param.Add("Id", request.Id);
                param.Add("UserPwd", EncryptDecrypt.Encrypt(request.UserPwd));
                param.Add("ModifiedById", request.ModifiedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_Settings", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserPWDById", ex.Message);
                throw;
            }
        }

        #endregion "usp_Settings"
    }
}