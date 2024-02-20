using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// StateRepository
    /// </summary>
    public class EmailSetupRepository : IEmailSetupRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<EmailSetupRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public EmailSetupRepository(ILogger<EmailSetupRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("EmailSetupRepository Initialized");
        }
        #endregion

        #region ===[ IEmailSetupRepository Methods ]==================================================

        public async Task<IEnumerable<EmailSetup>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<EmailSetup>("usp_EmailSetup", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<EmailSetup> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<EmailSetup>("usp_EmailSetup", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(EmailSetup emailSetup)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", emailSetup.Id);
            param.Add("IPAddress", emailSetup.IPAddress);
            param.Add("EmailId", emailSetup.EmailId);
            param.Add("EmailPWD", emailSetup.EmailPWD);
            param.Add("SMTPPort", emailSetup.SMTPPort);
            param.Add("IsActive", emailSetup.IsActive);
            param.Add("CreatedById", emailSetup.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);            

            var res = await _sqlConnection.ExecuteAsync("usp_EmailSetup", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(EmailSetup emailSetup)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", emailSetup.Id);
            param.Add("IPAddress", emailSetup.IPAddress);
            param.Add("EmailId", emailSetup.EmailId);
            param.Add("EmailPWD", emailSetup.EmailPWD);
            param.Add("SMTPPort", emailSetup.SMTPPort);
            param.Add("IsActive", emailSetup.IsActive);
            param.Add("ModifiedById", emailSetup.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_EmailSetup", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_EmailSetup", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}