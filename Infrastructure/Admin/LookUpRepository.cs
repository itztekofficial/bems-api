using Admin.Data.Sql.Queries;
using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Repositories.Admin
{
    /// <summary>
    /// LookUpRepository
    /// </summary>
    public class LookUpRepository : ILookUpRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<LookUpRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public LookUpRepository(ILogger<LookUpRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("LookUpRepository Initialized");
        }
        #endregion

        #region ===[ ILookUpRepository Methods ]==================================================
        public async Task<IEnumerable<LookUpModel>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<LookUpModel>("usp_Lookups", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<LookUpModel>> GetAllByLookTypeIdAsync(int lookTypeId)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getBylookTypeId");
            param.Add("LookTypeId", lookTypeId);

            return await _sqlConnection.QueryAsync<LookUpModel>("usp_Lookups", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<LookUpModel> GetByIdAsync(int id)
        {
            //Using Inline Query
            // return await _sqlConnection.QuerySingleOrDefaultAsync<LookUpModel>(LookUpQueries.LookUpById, new { Id = id }, transaction: _dbTransaction);

            //Using Store proc
            //use parameter like 1
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            //use parameter like 2
            //new { action = "getById", Id = id}

            return await _sqlConnection.QuerySingleOrDefaultAsync<LookUpModel>("usp_Lookups", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

       
        public async Task<bool> CreateAsync(LookUpModel entity)
        {
            var result = await _sqlConnection.ExecuteAsync(LookUpQueries.AddLookUp, entity, transaction: _dbTransaction);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(LookUpModel entity)
        {
            var result = await _sqlConnection.ExecuteAsync(LookUpQueries.UpdateLookUp, entity, transaction: _dbTransaction);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int Id, bool IsActive, int UpdatedById)
        {
            var result = await _sqlConnection.ExecuteAsync(LookUpQueries.DeleteLookUp, new { Id = Id }, transaction: _dbTransaction);
            return result > 0;
        }
        #endregion
    }
}