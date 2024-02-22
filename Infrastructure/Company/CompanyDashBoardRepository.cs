
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Company;

namespace Repositories.Company
{
    /// <summary>
    /// CompanyDashBoardRepository
    /// </summary>
    public class CompanyDashBoardRepository : ICompanyDashBoardRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<CompanyDashBoardRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public CompanyDashBoardRepository(ILogger<CompanyDashBoardRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("CompanyDashBoardRepository Initialized");
        }
        #endregion

        #region ===[ ICompanyDashBoardRepository Methods ]==================================================

        public async Task<CompanyDashBoardResponse> GetCompanyDashBoardData(CommonRequest request)
        {
            CompanyDashBoardResponse dashBoardResponse = new();

            var param = new DynamicParameters();
            param.Add("ActionType", "getDashBoardDetail");
            param.Add("UserId", request.UserId);
            param.Add("EntityId", request.EntityId);
            param.Add("DepartmentId", request.DepartmentId);
            param.Add("RoleTypeId", request.RoleTypeId);
            param.Add("RequestStatusIds", request.RequestStatusIds);

            var cmd = new CommandDefinition(AppConst.usp_CompanyDashBoard, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
            var res = await _sqlConnection.QueryMultipleAsync(cmd);

            dashBoardResponse.ContractCountResponse = res.Read();
            dashBoardResponse.MyRequestGraphRepsonse = res.ReadFirstOrDefault<MyRequestGraphRepsonse>();
            dashBoardResponse.MyContractResponses = res.Read<MyContractResponse>();

            return dashBoardResponse;
        }

        public async Task<IEnumerable<RequestHistoryResponse>> GetRequestHistory(string initiationId)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getRequestHistory");
            param.Add("Id", initiationId);

            var cmd = new CommandDefinition(AppConst.usp_CompanyDashBoard, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
            return await _sqlConnection.QueryAsync<RequestHistoryResponse>(cmd);
        }
        #endregion

    }
}