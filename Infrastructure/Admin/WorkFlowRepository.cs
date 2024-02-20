using Admin.Repositories.Contracts;
using Core.AppConst;
using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// WorkFlowRepository
    /// </summary>
    public class WorkFlowRepository : IWorkFlowRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<WorkFlowRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public WorkFlowRepository(ILogger<WorkFlowRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("WorkFlowRepository Initialized");
        }
        #endregion

        #region ===[ IWorkFlowRepository Methods ]==================================================
        public async Task<WorkFlowCombinedDataResponse> GetCombinedData()
        {
            WorkFlowCombinedDataResponse dataResponse = new();

            var param = new DynamicParameters();
            param.Add("ActionType", "getCombinedData");

            var cmd = new CommandDefinition("usp_Workflow", param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
            var res = await _sqlConnection.QueryMultipleAsync(cmd);

            dataResponse.EntityList = res.Read<CommonDropdown>();
            dataResponse.StatusTypeList = res.Read<CommonDropdown>();
            dataResponse.RoleTypeList = res.Read<CommonDropdown>();

            return dataResponse;
        }

        public async Task<IEnumerable<WorkFlow>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<WorkFlow>("usp_Workflow", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<WorkFlow> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<WorkFlow>("usp_Workflow", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(WorkFlow workflow)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", workflow.Id);
            param.Add("CompanyId", workflow.CompanyId == 0 ? 1 : workflow.CompanyId);
            param.Add("EntityId", workflow.EntityId);
            param.Add("RequestStatusId", workflow.RequestStatusId);
            param.Add("RoleTypeId", workflow.RoleTypeId);
            param.Add("ApproverRoleTypeId", workflow.ApproverRoleTypeId);
            param.Add("Sequence", workflow.Sequence);
            param.Add("IsActive", workflow.IsActive);
            param.Add("CreatedById", workflow.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            //param.Add("ModifiedById", workflow.UpdatedById);
            //param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Workflow", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(WorkFlow workflow)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", workflow.Id);
            param.Add("CompanyId", workflow.CompanyId == 0 ? 1 : workflow.CompanyId);
            param.Add("EntityId", workflow.EntityId);
            param.Add("RequestStatusId", workflow.RequestStatusId);
            param.Add("RoleTypeId", workflow.RoleTypeId);
            param.Add("ApproverRoleTypeId", workflow.ApproverRoleTypeId);
            param.Add("Sequence", workflow.Sequence);
            param.Add("IsActive", workflow.IsActive);
            param.Add("ModifiedById", workflow.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Workflow", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
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

            var res = await _sqlConnection.ExecuteAsync("usp_Workflow", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}