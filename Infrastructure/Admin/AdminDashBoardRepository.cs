using Admin.Repositories.Contracts;
using Core.DataModel;
using Core.Models.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Admin.Repositories
{
    /// <summary>
    /// AdminDashBoardRepository
    /// </summary>
    public class AdminDashBoardRepository : IAdminDashBoardRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<AdminDashBoardRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public AdminDashBoardRepository(ILogger<AdminDashBoardRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("AdminDashBoardRepository Initialized");
        }
        #endregion

        #region ===[ IAdminDashBoardRepository Methods ]==================================================

        public async Task<AdminDashBoardResponse> GetAdminDashBoardData()
        {
            AdminDashBoardResponse dashBoardResponse = new();

            var param = new DynamicParameters();
            param.Add("ActionType", "getDashBoardDetail");

            var res = await _sqlConnection.QueryMultipleAsync("usp_AdminDashBoard", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            dashBoardResponse.AllMastersCount = res.ReadFirstOrDefault<AllMastersCountResponse>();
            dashBoardResponse.ActivityLogDetails = res.Read<ActivityLog>();

            return dashBoardResponse;
        }

        #endregion
    }
}