using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// UserHelpFileMappingRepository
    /// </summary>
    public class UserHelpFileMappingRepository : IUserHelpFileMappingRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<UserHelpFileMappingRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public UserHelpFileMappingRepository(ILogger<UserHelpFileMappingRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("UserHelpFileMappingRepository Initialized");
        }
        #endregion

        #region ===[ IUserHelpFileMappingRepository Methods ]==================================================

        public async Task<IEnumerable<UserHelpFileMapping>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<UserHelpFileMapping>("usp_UserHelpFileMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<UserHelpFileMapping> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<UserHelpFileMapping>("usp_UserHelpFileMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(UserHelpFileMapping userHelpFileMapping)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", userHelpFileMapping.Id);
            param.Add("CompanyId", userHelpFileMapping.CompanyId == 0 ? 1 : userHelpFileMapping.CompanyId);
            param.Add("RoleTypeId", userHelpFileMapping.RoleTypeId);

            if (userHelpFileMapping.File != null && userHelpFileMapping.File.Length > 0)
            {
                param.Add("FileName", userHelpFileMapping.File.FileName);
                param.Add("FileExt", Path.GetExtension(userHelpFileMapping.File.FileName));
            }

            param.Add("Sequence", userHelpFileMapping.Sequence);
            param.Add("IsActive", userHelpFileMapping.IsActive);
            param.Add("CreatedById", userHelpFileMapping.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", userHelpFileMapping.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_UserHelpFileMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(UserHelpFileMapping userHelpFileMapping)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", userHelpFileMapping.Id);
            param.Add("CompanyId", userHelpFileMapping.CompanyId == 0 ? 1 : userHelpFileMapping.CompanyId);
            param.Add("RoleTypeId", userHelpFileMapping.RoleTypeId);

            if (userHelpFileMapping.File != null && userHelpFileMapping.File.Length > 0)
            {
                param.Add("FileName", userHelpFileMapping.File.FileName);
                param.Add("FileExt", Path.GetExtension(userHelpFileMapping.File.FileName));
            }

            param.Add("Sequence", userHelpFileMapping.Sequence);
            param.Add("IsActive", userHelpFileMapping.IsActive);
            param.Add("ModifiedById", userHelpFileMapping.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_UserHelpFileMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int Id, bool IsActive, int UpdatedById)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "delete");
            param.Add("Id", Id);
            param.Add("IsActive", IsActive);
            param.Add("ModifiedById", UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow); ;

            var res = await _sqlConnection.ExecuteAsync("usp_UserHelpFileMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}