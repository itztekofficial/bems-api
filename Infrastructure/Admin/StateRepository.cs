using System.Data;
using System.Data.SqlClient;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Admin;

namespace Repositories.Admin
{
    /// <summary>
    /// StateRepository
    /// </summary>
    public class StateRepository : IStateRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<StateRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public StateRepository(ILogger<StateRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("StateRepository Initialized");
        }
        #endregion

        #region ===[ ICountryRepository Methods ]==================================================

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<State>("usp_State", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<State> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<State>("usp_State", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(State state)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", state.Id);
            param.Add("CountryId", state.CountryId);
            param.Add("Code", state.Code);
            param.Add("Name", state.Name);
            param.Add("Sequence", state.Sequence);
            param.Add("IsActive", state.IsActive);
            param.Add("CreatedById", state.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_State", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(State state)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", state.Id);
            param.Add("CountryId", state.CountryId);
            param.Add("Code", state.Code);
            param.Add("Name", state.Name);
            param.Add("Sequence", state.Sequence);
            param.Add("IsActive", state.IsActive);
            param.Add("ModifiedById", state.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_State", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_State", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}