using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// ProductRepository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<ProductRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public ProductRepository(ILogger<ProductRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("Product Repository Initialized");
        }
        #endregion

        #region ===[ IProductRepository Methods ]==================================================

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<Product>("usp_Product", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<Product>("usp_Product", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(Product product)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", product.Id);
            param.Add("CompanyId", product.CompanyId == 0 ? 1 : product.CompanyId);
            param.Add("Code", product.Code);
            param.Add("Name", product.Name);
            param.Add("Sequence", product.Sequence);
            param.Add("IsActive", product.IsActive);
            param.Add("CreatedById", product.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", product.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Product", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", product.Id);
            param.Add("CompanyId", product.CompanyId == 0 ? 1 : product.CompanyId);
            param.Add("Code", product.Code);
            param.Add("Name", product.Name);
            param.Add("Sequence", product.Sequence);
            param.Add("IsActive", product.IsActive);
            param.Add("ModifiedById", product.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Product", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_Product", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}