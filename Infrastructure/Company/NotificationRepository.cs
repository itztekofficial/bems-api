using Company.Repositories.Contracts;
using Core.AppConst;
using Core.Models.Request;
using Core.Models.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Repositories.Company
{
    /// <summary>
    /// NotificationRepository
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<NotificationRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public NotificationRepository(ILogger<NotificationRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("NotificationRepository Initialized");
        }
        #endregion

        #region ===[ NotificationRepository Methods ]==================================================            

        public async Task<IEnumerable<NotificationResponse>> GetNotificationListAsync(NotificationRequest notificationRequest)
        {
            var param = new DynamicParameters();
            param.Add("Id", notificationRequest.Id);
            param.Add("ActionType", notificationRequest.ActionType);
            param.Add("EntityId", notificationRequest.EntityId);
            param.Add("DepartmentId", notificationRequest.DepartmentId);
            param.Add("Month", notificationRequest.Month);
            param.Add("Year", notificationRequest.Year);

            var cmd = new CommandDefinition(AppConst.usp_Notifications, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
            return await _sqlConnection.QueryAsync<NotificationResponse>(cmd);
        }

        #endregion
    }
}