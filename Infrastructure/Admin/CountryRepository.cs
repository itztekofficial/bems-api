using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// CountryRepository
    /// </summary>
    public class CountryRepository : ICountryRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<CountryRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public CountryRepository(ILogger<CountryRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("CountryRepository Initialized");
        }
        #endregion

        #region ===[ ICountryRepository Methods ]==================================================

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<Country>("usp_Country", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<Country>("usp_Country", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(Country cont)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", cont.Id);
            param.Add("Code", cont.Code);
            param.Add("Name", cont.Name);
            param.Add("Sequence", cont.Sequence);
            param.Add("IsActive", cont.IsActive);
            param.Add("CreatedById", cont.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);            

            var res = await _sqlConnection.ExecuteAsync("usp_Country", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(Country cont)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", cont.Id);
            param.Add("Code", cont.Code);
            param.Add("Name", cont.Name);
            param.Add("Sequence", cont.Sequence);
            param.Add("IsActive", cont.IsActive);
            param.Add("ModifiedById", cont.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Country", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_Country", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}