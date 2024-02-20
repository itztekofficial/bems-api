using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// EmailTemplate
    /// </summary>
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<EmailTemplateRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public EmailTemplateRepository(ILogger<EmailTemplateRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("EmailTemplateRepository Initialized");
        }
        #endregion

        #region ===[ IEmailTemplateRepository Methods ]==================================================

        public async Task<IEnumerable<EmailTemplate>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<EmailTemplate>("usp_EmailTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<EmailTemplate> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<EmailTemplate>("usp_EmailTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(EmailTemplate emailTemplate)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", emailTemplate.Id);
            param.Add("MailTypeId", emailTemplate.MailTypeId);
            param.Add("Template", emailTemplate.Template);
            param.Add("IsActive", emailTemplate.IsActive);
            param.Add("CreatedById", emailTemplate.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);            

            var res = await _sqlConnection.ExecuteAsync("usp_EmailTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(EmailTemplate emailTemplate)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", emailTemplate.Id);
            param.Add("MailTypeId", emailTemplate.MailTypeId);
            param.Add("Template", emailTemplate.Template);
            param.Add("IsActive", emailTemplate.IsActive);
            param.Add("ModifiedById", emailTemplate.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_EmailTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_EmailTemplate", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}