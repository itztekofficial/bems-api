using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// PaymentTermRepository
    /// </summary>
    public class PaymentTermRepository : IPaymentTermRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<PaymentTermRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public PaymentTermRepository(ILogger<PaymentTermRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("PaymentTermRepository Initialized");
        }
        #endregion

        #region ===[ IPaymentTermRepository Methods ]==================================================

        public async Task<IEnumerable<PaymentTerm>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<PaymentTerm>("usp_PaymentTerm", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<PaymentTerm> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<PaymentTerm>("usp_PaymentTerm", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(PaymentTerm paymentTerm)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", paymentTerm.Id);
            param.Add("CompanyId", paymentTerm.CompanyId == 0 ? 1 : paymentTerm.CompanyId);
            param.Add("Name", paymentTerm.Name);
            param.Add("Sequence", paymentTerm.Sequence);
            param.Add("IsActive", paymentTerm.IsActive);
            param.Add("CreatedById", paymentTerm.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", paymentTerm.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_PaymentTerm", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(PaymentTerm paymentTerm)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", paymentTerm.Id);
            param.Add("CompanyId", paymentTerm.CompanyId == 0 ? 1 : paymentTerm.CompanyId);
            param.Add("Name", paymentTerm.Name);
            param.Add("Sequence", paymentTerm.Sequence);
            param.Add("IsActive", paymentTerm.IsActive);
            param.Add("ModifiedById", paymentTerm.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_PaymentTerm", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_PaymentTerm", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}