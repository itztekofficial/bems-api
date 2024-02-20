using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// ContractTemplateRepository
    /// </summary>
    public class ContractTemplateRepository : IContractTemplateRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<ContractTemplateRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public ContractTemplateRepository(ILogger<ContractTemplateRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("ContractTemplateRepository Initialized");
        }
        #endregion

        #region ===[ IContractTemplateRepository Methods ]==================================================

        public async Task<IEnumerable<ContractTemplate>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<ContractTemplate>("usp_ContractTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<ContractTemplate> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<ContractTemplate>("usp_ContractTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(ContractTemplate contractTemplate)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", contractTemplate.Id);
            param.Add("CompanyId", contractTemplate.CompanyId == 0 ? 1 : contractTemplate.CompanyId);
            if (contractTemplate.File != null && contractTemplate.File.Length > 0)
            {
                param.Add("FileName", contractTemplate.File.FileName);
                param.Add("FileExt", Path.GetExtension(contractTemplate.File.FileName));
            }
            param.Add("Sequence", contractTemplate.Sequence);
            param.Add("IsActive", contractTemplate.IsActive);
            param.Add("CreatedById", contractTemplate.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", contractTemplate.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_ContractTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(ContractTemplate contractTemplate)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", contractTemplate.Id);
            param.Add("CompanyId", contractTemplate.CompanyId == 0 ? 1 : contractTemplate.CompanyId);
               if (contractTemplate.File != null && contractTemplate.File.Length > 0)
            {
                param.Add("FileName", contractTemplate.File.FileName);
                param.Add("FileExt", Path.GetExtension(contractTemplate.File.FileName));
            }
            param.Add("Sequence", contractTemplate.Sequence);
            param.Add("IsActive", contractTemplate.IsActive);
            param.Add("ModifiedById", contractTemplate.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_ContractTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_ContractTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}