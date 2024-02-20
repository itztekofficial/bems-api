using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// EntityTypeRepository
    /// </summary>
    public class EntityTypeRepository : IEntityTypeRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<EntityTypeRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public EntityTypeRepository(ILogger<EntityTypeRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("EntitytypeRepository Initialized");
        }
        #endregion

        #region ===[ IEntityRepository Methods ]==================================================

        public async Task<IEnumerable<EntityType>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<EntityType>("usp_EntityType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<EntityType> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<EntityType>("usp_EntityType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(EntityType entitytype)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", entitytype.Id);
            param.Add("CompanyId", entitytype.CompanyId == 0 ? 1 : entitytype.CompanyId);
            param.Add("Name", entitytype.Name);
            param.Add("Sequence", entitytype.Sequence);
            param.Add("IsActive", entitytype.IsActive);
            param.Add("CreatedById", entitytype.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", entitytype.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_EntityType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(EntityType entitytype)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", entitytype.Id);
            param.Add("CompanyId", entitytype.CompanyId == 0 ? 1 : entitytype.CompanyId);
            param.Add("Name", entitytype.Name);
            param.Add("Sequence", entitytype.Sequence);
            param.Add("IsActive", entitytype.IsActive);
            param.Add("ModifiedById", entitytype.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_EntityType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_EntityType", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}