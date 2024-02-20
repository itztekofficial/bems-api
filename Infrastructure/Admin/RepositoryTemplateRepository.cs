using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// RepositoryTemplateRepository
    /// </summary>
    public class RepositoryTemplateRepository : IRepositoryTemplateRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<RepositoryTemplateRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public RepositoryTemplateRepository(ILogger<RepositoryTemplateRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("RepositoryTemplateRepository Initialized");
        }
        #endregion

        #region ===[ IRepositoryTemplateRepository Methods ]==================================================

        public async Task<IEnumerable<RepositoryTemplate>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<RepositoryTemplate>("usp_RepositoryTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<RepositoryTemplate> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<RepositoryTemplate>("usp_RepositoryTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(RepositoryTemplate repositoryTemplate)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", repositoryTemplate.Id);
            param.Add("CompanyId", repositoryTemplate.CompanyId == 0 ? 1 : repositoryTemplate.CompanyId);
            if (repositoryTemplate.File != null && repositoryTemplate.File.Length > 0)
            {
                param.Add("FileName", repositoryTemplate.File.FileName);
                param.Add("FileExt", Path.GetExtension(repositoryTemplate.File.FileName));
            }
            param.Add("Sequence", repositoryTemplate.Sequence);
            param.Add("IsActive", repositoryTemplate.IsActive);
            param.Add("CreatedById", repositoryTemplate.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", repositoryTemplate.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_RepositoryTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(RepositoryTemplate repositoryTemplate)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", repositoryTemplate.Id);
            param.Add("CompanyId", repositoryTemplate.CompanyId == 0 ? 1 : repositoryTemplate.CompanyId);
               if (repositoryTemplate.File != null && repositoryTemplate.File.Length > 0)
            {
                param.Add("FileName", repositoryTemplate.File.FileName);
                param.Add("FileExt", Path.GetExtension(repositoryTemplate.File.FileName));
            }
            param.Add("Sequence", repositoryTemplate.Sequence);
            param.Add("IsActive", repositoryTemplate.IsActive);
            param.Add("ModifiedById", repositoryTemplate.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_RepositoryTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_RepositoryTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}