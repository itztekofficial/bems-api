using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// SubAgreementRepository
    /// </summary>
    public class SubAgreementRepository : ISubAgreementRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<SubAgreementRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public SubAgreementRepository(ILogger<SubAgreementRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("SubAgreementRepository Initialized");
        }
        #endregion

        #region ===[ ISubAgreementRepository Methods ]==================================================

        public async Task<IEnumerable<SubAgreement>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<SubAgreement>("usp_SubAgreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<SubAgreement> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<SubAgreement>("usp_SubAgreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(SubAgreement subAgreement)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", subAgreement.Id);
            param.Add("CompanyId", subAgreement.CompanyId == 0 ? 1 : subAgreement.CompanyId);
            param.Add("Name", subAgreement.Name);
            param.Add("Sequence", subAgreement.Sequence);
            param.Add("IsActive", subAgreement.IsActive);
            param.Add("CreatedById", subAgreement.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", subAgreement.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_SubAgreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(SubAgreement subAgreement)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", subAgreement.Id);
            param.Add("CompanyId", subAgreement.CompanyId == 0 ? 1 : subAgreement.CompanyId);
            param.Add("Name", subAgreement.Name);
            param.Add("Sequence", subAgreement.Sequence);
            param.Add("IsActive", subAgreement.IsActive);
            param.Add("ModifiedById", subAgreement.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_SubAgreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_SubAgreement", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}