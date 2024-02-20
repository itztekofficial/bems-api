using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// AgreementRepository
    /// </summary>
    public class AgreementRepository : IAgreementRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<AgreementRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public AgreementRepository(ILogger<AgreementRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("AgreementRepository Initialized");
        }
        #endregion

        #region ===[ IAgreementRepository Methods ]==================================================

        public async Task<IEnumerable<Agreement>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<Agreement>("usp_Agreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Agreement> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<Agreement>("usp_Agreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(Agreement agreement)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", agreement.Id);
            param.Add("CompanyId", agreement.CompanyId == 0 ? 1 : agreement.CompanyId);
            param.Add("Name", agreement.Name);
            param.Add("Sequence", agreement.Sequence);
            param.Add("IsActive", agreement.IsActive);
            param.Add("CreatedById", agreement.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", agreement.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Agreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(Agreement agreement)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", agreement.Id);
            param.Add("CompanyId", agreement.CompanyId == 0 ? 1 : agreement.CompanyId);
            param.Add("Name", agreement.Name);
            param.Add("Sequence", agreement.Sequence);
            param.Add("IsActive", agreement.IsActive);
            param.Add("ModifiedById", agreement.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Agreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_Agreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}