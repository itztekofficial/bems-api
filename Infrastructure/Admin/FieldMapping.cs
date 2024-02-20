using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// EntityRepository
    /// </summary>
    public class FieldMappingRepository : IFieldMappingRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<FieldMappingRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public FieldMappingRepository(ILogger<FieldMappingRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("FieldMapping Initialized");
        }
        #endregion

        #region ===[ IEntityRepository Methods ]==================================================

        public async Task<IEnumerable<FieldMapping>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<FieldMapping>("usp_FieldMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<FieldMapping> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<FieldMapping>("usp_FieldMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(FieldMapping fieldMapping)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", fieldMapping.Id);
            param.Add("CompanyId", fieldMapping.CompanyId == 0 ? 1 : fieldMapping.CompanyId);
            param.Add("Name", fieldMapping.Name);
            param.Add("IsVisible", fieldMapping.IsVisible);
            param.Add("IsRequired", fieldMapping.IsRequired);
            param.Add("Sequence", fieldMapping.Sequence);
            param.Add("IsActive", fieldMapping.IsActive);
            param.Add("CreatedById", fieldMapping.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", fieldMapping.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_FieldMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(FieldMapping fieldMapping)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", fieldMapping.Id);
            param.Add("CompanyId", fieldMapping.CompanyId == 0 ? 1 : fieldMapping.CompanyId);
            param.Add("Name", fieldMapping.Name);
            param.Add("IsVisible", fieldMapping.IsVisible);
            param.Add("IsRequired", fieldMapping.IsRequired);
            param.Add("Sequence", fieldMapping.Sequence);
            param.Add("IsActive", fieldMapping.IsActive);
            param.Add("ModifiedById", fieldMapping.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_FieldMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_FieldMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}