using System.Data;
using System.Data.SqlClient;
using Core.DataModel;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Login;

namespace Login.Repositories
{
    public class UtilRepository : IUtilRepository
    {
        #region ===[ Private Members ]===================================

        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<UtilRepository> _logger;

        #endregion ===[ Private Members ]===================================

        #region ===[ Constructor ]=======================================

        public UtilRepository(ILogger<UtilRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("UtilRepository Initialized");
        }

        #endregion ===[ Constructor ]=======================================

        #region ===[ UtilRepository Methods ]==================================================

        /// <summary>
        /// SaveOTP
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        public async Task<bool> SaveOTP(Otp otp)
        {
            try
            {
                //_mongoDBHelper.CreateDBConnection(new Database { DatabaseType = DatabaseType.MargConnect });

                //FilterDefinition<Otp> filter = Builders<Otp>.Filter.Eq("mobileNo", otp.mobileNo);
                //var data = _mongoDBHelper.Select("Otp", filter).Result;
                //if (data != null && data.Count > 0)
                //{
                //    //filter &= Builders<Otp>.Filter.Eq("isactive", true);
                //    var updateDefinition = Builders<Otp>.Update.Set(p => p.isactive, false).Set(p => p.lastupdated, otp.lastupdated);
                //    await _mongoDBHelper.UpdateMany("Otp", filter, updateDefinition);

                //    //data.ForEach(x =>
                //    //{
                //    //    _mongoDBHelper.UpdateOne("Otp", filter, updateDefinition);
                //    //});
                //}

                //return await _mongoDBHelper.Insert("Otp", otp);

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilRepository", "SaveOTP", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// ValidateOTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ValidateOTP(ValidateOTPRequest request)
        {
            try
            {
                //_mongoDBHelper.CreateDBConnection(new Database { DatabaseType = DatabaseType.MargConnect });

                //FilterDefinition<Otp> filter = Builders<Otp>.Filter.Eq("mobileNo", request.mobileNo);
                //filter &= Builders<Otp>.Filter.Eq("isactive", true);
                //filter &= Builders<Otp>.Filter.Eq("mobileOTP", request.otp);

                //var data = _mongoDBHelper.Select("Otp", filter).Result;
                //isFound = (data != null && data.Count > 0) ? true : false;

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilRepository", "ValidateOTP", ex.Message);
                throw;
            }
        }

        #endregion ===[ UtilRepository Methods ]==================================================
    }
}