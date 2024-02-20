using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// CityRepository
    /// </summary>
    public class CityRepository : ICityRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<CityRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public CityRepository(ILogger<CityRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("CityRepository Initialized");
        }
        #endregion

        #region ===[ ICountryRepository Methods ]==================================================

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<City>("usp_City", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<City> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<City>("usp_City", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(City ct)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", ct.Id);
            param.Add("StateId", ct.StateId);
            param.Add("Code", ct.Code);
            param.Add("Name", ct.Name);
            param.Add("Sequence", ct.Sequence);
            param.Add("IsActive", ct.IsActive);
            param.Add("CreatedById", ct.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);            

            var res = await _sqlConnection.ExecuteAsync("usp_City", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(City ct)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", ct.Id); 
            param.Add("StateId", ct.StateId);
            param.Add("Code", ct.Code);
            param.Add("Name", ct.Name);
            param.Add("Sequence", ct.Sequence);
            param.Add("IsActive", ct.IsActive);
            param.Add("ModifiedById", ct.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_City", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_City", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}