using System.Data;
using System.Data.SqlClient;
using Core.Models.Request;
using Core.Models.Response;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Admin;


namespace Repositories.Admin
{
    /// <summary>
    /// CompanyRepository
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<CompanyRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public CompanyRepository(ILogger<CompanyRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("CompanyRepository Initialized");
        }
        #endregion

        #region ===[ ICompanyRepository Methods ]==================================================
        /// <summary>
        /// GetCompanyDetail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CompanyResponse> GetCompanyDetail(string Id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                //   _logger.WriteLog("CompanyRepository", "GetCompanyDetail", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public async Task<int> Save(CompanyRequest Company)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                // _logger.WriteLog("CompanyRepository", "Save", ex.Message);
                return 3;
            }
        }

        #region "UserConfirmationMail"

        /// <summary>
        /// UserConfirmationMail
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="companyName"></param>
        /// <param name="emailId"></param>
        /// <param name="pass"></param>
        private static void UserConfirmationMail(string sName, string companyName, string emailId, string pass)
        {
            if (!string.IsNullOrEmpty(emailId))
            {
                try
                {
                    // StringBuilder sb = new();

                    //  sb.Append("Test mail");

                    //string emailResponse = new MailSMS().Send(emailId, "Welcome !", sb.ToString());
                }
                catch { }
            }
        }

        #endregion "UserConfirmationMail"

        /// <summary>
        /// check email or mobile is already exist
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> EmailMobileExistValid(string Type, string val)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                //_logger.WriteLog("CompanyRepository", "Save", ex.Message);
                return 2;
            }
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CompanyResponse>> GetList()
        {
            //var result = await _sqlConnection.QueryAsync<LookUpModel>("usp_Lookups", new { action = "getAll" }, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            throw new NotImplementedException();
        }

        //public Task<int> Save(CompanyRequest Company)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}