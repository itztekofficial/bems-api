using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Company;

namespace Repositories.Company
{
    /// <summary>
    /// ReportRepositoryRepository
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<ReportRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public ReportRepository(ILogger<ReportRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("ReportRepository Initialized");
        }
        #endregion

        #region =======[ ReportRepositoryRepository Methods ]============================
        //public async Task<IEnumerable<ApprovalResponse>> GetAllContractsAsync(ReportRequest reportRequest)  Manoj
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("ActionType", "getAllContracts");
        //        param.Add("UserId", reportRequest.UserId);
        //        param.Add("EntityId", reportRequest.EntityId);
        //        param.Add("DepartmentId", reportRequest.DepartmentId);
        //        param.Add("RoleTypeId", reportRequest.RoleTypeId);
        //        param.Add("RequestStatusIds", reportRequest.RequestStatusIds);

        //        var cmd = new CommandDefinition(AppConst.usp_Report, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);

        //        return await _sqlConnection.QueryAsync<ApprovalResponse>(cmd);
        //    }
        //    catch { throw; }
        //}

        public async Task<IEnumerable<ContractReportResponse>> GetExportAllContractsAsync(ReportRequest reportRequest)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getInitiationReport");
                param.Add("UserId", reportRequest.UserId);
                param.Add("EntityId", reportRequest.EntityId);
                param.Add("DepartmentId", reportRequest.DepartmentId);
                param.Add("RoleTypeId", reportRequest.RoleTypeId);
                param.Add("RequestStatusIds", reportRequest.RequestStatusIds);

                var cmd = new CommandDefinition(AppConst.usp_Report, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);

                return await _sqlConnection.QueryAsync<ContractReportResponse>(cmd);
            }
            catch { throw; }
        }

        #endregion
    }
}