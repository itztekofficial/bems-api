using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Repositories.Admin
{
    /// <summary>
    /// DocumentRepository
    /// </summary>
    public class DocumentRepository : IDocumentRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<DocumentRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public DocumentRepository(ILogger<DocumentRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("DocumentRepository Initialized");
        }
        #endregion

        #region ===[ IDocumentRepository Methods ]==================================================

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<Document>("usp_Document", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Document> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<Document>("usp_Document", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(Document document)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", document.Id);
            param.Add("CompanyId", document.CompanyId == 0 ? 1 : document.CompanyId);
            param.Add("EntityTypeId", document.EntityTypeId);
            param.Add("CustomerTypeId", document.CustomerTypeId);
            param.Add("Name", document.Name);
            param.Add("Sequence", document.Sequence);
            param.Add("IsActive", document.IsActive);
            param.Add("CreatedById", document.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", document.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Document", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(Document document)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", document.Id);
            param.Add("CompanyId", document.CompanyId == 0 ? 1 : document.CompanyId);
            param.Add("EntityTypeId", document.EntityTypeId);
            param.Add("CustomerTypeId", document.CustomerTypeId);
            param.Add("Name", document.Name);
            param.Add("Sequence", document.Sequence);
            param.Add("IsActive", document.IsActive);
            param.Add("ModifiedById", document.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Document", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_Document", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}