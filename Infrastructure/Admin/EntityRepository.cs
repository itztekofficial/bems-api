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
    public class EntityRepository : IEntityRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<EntityRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public EntityRepository(ILogger<EntityRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("EntityRepository Initialized");
        }
        #endregion

        #region ===[ IEntityRepository Methods ]==================================================

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<Entity>("usp_Entity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Entity> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<Entity>("usp_Entity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(Entity entity)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", entity.Id);
            param.Add("CompanyId", entity.CompanyId == 0 ? 1 : entity.CompanyId);
            param.Add("Code", entity.Code);
            param.Add("Name", entity.Name);
            param.Add("Sequence", entity.Sequence);
            param.Add("IsActive", entity.IsActive);
            param.Add("CreatedById", entity.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", entity.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Entity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(Entity entity)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", entity.Id);
            param.Add("CompanyId", entity.CompanyId == 0 ? 1 : entity.CompanyId);
            param.Add("Code", entity.Code);
            param.Add("Name", entity.Name);
            param.Add("Sequence", entity.Sequence);
            param.Add("IsActive", entity.IsActive);
            param.Add("ModifiedById", entity.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Entity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_Entity", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}