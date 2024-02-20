using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Agreement;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// ActivityLogRepository
    /// </summary>
    public class ActivityLogRepository : IActivityLogRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<ActivityLogRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public ActivityLogRepository(ILogger<ActivityLogRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("ActivityLogRepository Initialized");
        }
        #endregion

        #region ===[ IActivityLogRepository Methods ]==================================================

        public async Task<IEnumerable<ActivityLog>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<ActivityLog>("usp_Activity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActivityLog> GetByIdAsync(int id)
        {
           throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(ActivityLog activity)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", activity.Id);
            param.Add("CompanyId", activity.CompanyId == 0 ? 1 : activity.CompanyId);
            param.Add("Module", activity.Module);
            param.Add("Action", activity.Action);
            param.Add("Message", activity.Message);
            param.Add("IsActive", activity.IsActive);
            param.Add("CreatedById", activity.Id);
            param.Add("CreateDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Activity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(ActivityLog activity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id, bool IsActive, int UpdatedById)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}