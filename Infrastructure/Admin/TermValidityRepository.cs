using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// TermValidityRepository
    /// </summary>
    public class TermValidityRepository : ITermValidityRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<TermValidityRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public TermValidityRepository(ILogger<TermValidityRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("TermValidity Repository Initialized");
        }
        #endregion

        #region ===[ ITermValidityRepository Methods ]==================================================

        public async Task<IEnumerable<TermValidity>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<TermValidity>("usp_TermValidity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<TermValidity> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<TermValidity>("usp_TermValidity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(TermValidity termValidity)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", termValidity.Id);
            param.Add("CompanyId", termValidity.CompanyId == 0 ? 1 : termValidity.CompanyId);            
            param.Add("Name", termValidity.Name);
            param.Add("Sequence", termValidity.Sequence);
            param.Add("IsActive", termValidity.IsActive);
            param.Add("CreatedById", termValidity.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", termValidity.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_TermValidity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(TermValidity termValidity)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", termValidity.Id);
            param.Add("CompanyId", termValidity.CompanyId == 0 ? 1 : termValidity.CompanyId);            
            param.Add("Name", termValidity.Name);
            param.Add("Sequence", termValidity.Sequence);
            param.Add("IsActive", termValidity.IsActive);
            param.Add("ModifiedById", termValidity.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_TermValidity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_TermValidity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}