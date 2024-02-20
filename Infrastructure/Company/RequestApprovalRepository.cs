using Company.Repositories.Contracts;
using Core.AppConst;
using Core.Models.Request;
using Core.Models.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Company.Repositories
{
    /// <summary>
    /// RequestApprovalRepository
    /// </summary>
    public class RequestApprovalRepository : IRequestApprovalRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<RequestApprovalRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public RequestApprovalRepository(ILogger<RequestApprovalRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("RequestApprovalRepository Initialized");
        }
        #endregion

        #region ===[ RequestApprovalRepository Methods ]==================================================
        public async Task<IEnumerable<ApprovalResponse>> GetApprovalListAsync(ApprovalRequest approvalRequest)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getApprovalList");
            param.Add("Id", approvalRequest.Id);
            param.Add("EntityId", approvalRequest.EntityId);
            param.Add("DepartmentId", approvalRequest.DepartmentId);
            param.Add("RoleTypeId", approvalRequest.RoleTypeId);
            param.Add("RequestStatusIds", approvalRequest.RequestStatusIds);

            var cmd = new CommandDefinition(AppConst.usp_RequestApproval, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
            return await _sqlConnection.QueryAsync<ApprovalResponse>(cmd);
        }

        #endregion
    }
}