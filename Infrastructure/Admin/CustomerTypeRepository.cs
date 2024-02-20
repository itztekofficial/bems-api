using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// ICustomerTypeRepository
    /// </summary>
    public class CustomerTypeRepository : ICustomerTypeRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<CustomerTypeRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public CustomerTypeRepository(ILogger<CustomerTypeRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("Customer Type Repository Initialized");
        }
        #endregion

        #region ===[ ICustomerTypeRepository Methods ]==================================================

        public async Task<IEnumerable<CustomerType>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<CustomerType>("usp_CustomerType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<CustomerType> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<CustomerType>("usp_CustomerType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(CustomerType customerType)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", customerType.Id);
            param.Add("CompanyId", customerType.CompanyId == 0 ? 1 : customerType.CompanyId);            
            param.Add("Name", customerType.Name);
            param.Add("Sequence", customerType.Sequence);
            param.Add("IsActive", customerType.IsActive);
            param.Add("CreatedById", customerType.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", customerType.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_CustomerType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(CustomerType customerType)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", customerType.Id);
            param.Add("CompanyId", customerType.CompanyId == 0 ? 1 : customerType.CompanyId);
            param.Add("Name", customerType.Name);
            param.Add("Sequence", customerType.Sequence);
            param.Add("IsActive", customerType.IsActive);
            param.Add("ModifiedById", customerType.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_CustomerType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_CustomerType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}