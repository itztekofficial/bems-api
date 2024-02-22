using System.Data;
using System.Data.SqlClient;
using Core.DataModel;
using Dapper;
using Domain;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Login;

namespace Login.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        #region ===[ Private Members ]===================================

        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<LoginRepository> _logger;

        #endregion ===[ Private Members ]===================================

        #region ===[ Constructor ]=======================================

        public LoginRepository(ILogger<LoginRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("LoginRepository Initialized");
        }

        #endregion ===[ Constructor ]=======================================

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="request">LoginRequest</param>
        /// <returns>User</returns>
        public async Task<User> GetUserDetails(LoginRequest request)
        {
            User user = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "ValidateUser");
                param.Add("EmailId", request.userid);
                param.Add("UserPwd", EncryptDecrypt.Encrypt(request.password));

                user = await _sqlConnection.QueryFirstOrDefaultAsync<User>("usp_Login", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

                ////user = res.FirstOrDefault();
                //if (user.Name != null && user.Name != "")
                //{
                //    MailSMS mail = new MailSMS();
                //    EmailHtmlBodyData emaildata = new EmailHtmlBodyData();
                //    emaildata.Header = "Login Confirmation !";
                //    emaildata.WelcomeMessage = "Dear " + user.Name + " ,";
                //    // emaildata.MailBodyMessage = "Initiation contract request has been successfully submitting";
                //    emaildata.MailBodyMessage = "You are successfully logged in";
                //    emaildata.IdMessage = "";
                //    emaildata.IdNumber = "";
                //    string htmldata = mail.CrateEmailHTMLTemplate(emaildata);
                //    mail.SendEMail("mkmanoo@gmail.com", "Login Confirmation", htmldata.ToString(), "File name", null);
                //    // Email Testing Code END		
                //}

            }
            catch (Exception ex)
            {
                // Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
            }
            return user;
        }

        /// <summary>
        /// Validate user details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ValidateUserMultiRoleResponse> ValidateUserDetailById(int id)
        {
            ValidateUserMultiRoleResponse validateUserMultiRoleResponse = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "ValidateUserRole");
                param.Add("Id", id);

                using var multi = await _sqlConnection.QueryMultipleAsync("usp_Login", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

                validateUserMultiRoleResponse.UserMultiRoleResponse = await multi.ReadFirstOrDefaultAsync<UserMultiRoleResponse>();
                validateUserMultiRoleResponse.EntityList = await multi.ReadAsync<CommonDropdown>();
                validateUserMultiRoleResponse.DepartmentList = await multi.ReadAsync<CommonDropdown>();
            }
            catch (Exception ex)
            {
                //  Log.WriteLog("LoginRepository", "ValidateUserDetailById", ex.Message);
                throw;
            }
            return validateUserMultiRoleResponse;
        }

        /// <summary>
        /// GetUserDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginDetailResponse> GetUserDetailById(LoginMultiUserRequest request)
        {
            LoginDetailResponse loginDetailResponse = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "UserDetailById");
                param.Add("Id", request.Id);
                param.Add("EntityId", request.EntityId);
                param.Add("DepartmentId", request.DepartmentId);

                using var multi = await _sqlConnection.QueryMultipleAsync("usp_Login", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                var userData = await multi.ReadFirstOrDefaultAsync<UserWithRole>();
                var companyData = await multi.ReadFirstOrDefaultAsync<Company>();
                var menuList = await multi.ReadAsync<Menu>();

                if (menuList != null && menuList.Any())
                {
                    loginDetailResponse.UserData = new UserDetail
                    {
                        Id = userData.Id,
                        Name = userData.Name,
                        EmailId = userData.EmailId,
                        MobileNo = userData.MobileNo,
                        UserType = userData.UserType,
                        RoleId = userData.RoleId,
                        DepartmentId = userData.DepartmentId,
                        EntityId = userData.EntityId,
                    };
                    loginDetailResponse.MenuData = menuList.ToList();
                    loginDetailResponse.CompanyData = new CompanyResponse()
                    {
                        Id = companyData.Id,
                        Code = companyData.Code,
                        Name = companyData.Name,
                        ContactPerson = companyData.ContactPerson,
                        EmailId = companyData.EmailId,
                        RegMobile = companyData.RegMobile,
                        Address = companyData.Address,
                        GSTNNo = companyData.GSTNNo
                    };
                }

                //    //---Update user--
                //    var updateDefinition = Builders<User>.Update.Set(p => p.isloggedin, true).Set(p => p.logoutdatetime, null);
                //    await _mongoDBHelper.UpdateOne("User", filter, updateDefinition);

                //    LoginLog(loginDetailResponse);
            }
            catch (Exception ex)
            {
                //  Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
                throw;
            }
            return loginDetailResponse;
        }

        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User</returns>
        public async Task<User> GetUserDetailById(int id)
        {
            User user = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "GetUserDetailsById");
                param.Add("Id", id);
                user = await _sqlConnection.QueryFirstOrDefaultAsync<User>("usp_Login", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //  Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
            }
            return user;
        }

        /// <summary>
        /// GetUserDetailsByEmail
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns>string</returns>
        //public async Task<ForgotPasswordResponse> GetUserDetailsByEmail(string emailId)
        //{
        //    ForgotPasswordResponse response = new();
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("ActionType", "GetUserDetailsByEmailId");
        //        param.Add("EmailId", emailId);
        //        var userObj = await _sqlConnection.QueryFirstOrDefaultAsync<ForgotPassword>("usp_Login", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

        //        if (userObj != null)
        //        {
        //            Random generator = new();
        //            response.Name = userObj.Name;
        //            response.UserPwd = EncryptDecrypt.Decrypt(userObj.UserPwd);
        //            response.RandomCode = generator.Next(0, 1000000).ToString("D6");
        //            try
        //            {
        //                MailSMS mail = new();
        //                EmailHtmlBodyData emaildata = new()
        //                {
        //                    Header = "",
        //                    WelcomeMessage = "Dear " + userObj.Name + " ,",
        //                    MailBodyMessage = "We received your forgot your password request, Please enter security code <b>" + response.RandomCode + "</b> and proceed further to receive password. <br /><br /><br /> Note: Please do not share your security code and password to any one.",
        //                    IdMessage = "",
        //                    IdNumber = ""
        //                };
        //                string htmldata = mail.CrateEmailHTMLTemplate(emaildata);
        //                mail.SendEMail(emailId, "Forgot password request", htmldata.ToString(), string.Empty, null);
        //            }
        //            catch { }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
        //    }
        //    return response;
        //}

        /// <summary>
        /// LogoutUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> LogoutUser(int id)
        {
            try
            {
                //_mongoDBHelper.CreateDBConnection(new Database { DatabaseType = DatabaseType.MargConnect });

                //FilterDefinition<User> filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
                //var updateDefinition = Builders<User>.Update.Set(p => p.isloggedin, false).Set(p => p.logoutdatetime, DateTime.Now);
                //await _mongoDBHelper.UpdateOne("User", filter, updateDefinition);

                //LogoutLog(id);

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                //  Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
                throw;
            }
        }

        #region "Login & Logout Log"

        //private static void LoginLog(LoginDetailResponse Req)
        //{
        //    ApplicationLoginLogModel appLogEvent = new()
        //    {
        //        CompId = Req.companyData.id,
        //        UserId = Req.userData.id,
        //        CompName = "",
        //        UserName = Req.userData.name,
        //        Ip = "",
        //        Device = "Browser",
        //        LoginTime = DateTime.Now,
        //        LogoutTime = DateTime.Now,
        //        Latitude = "",
        //        Longitude = "",
        //        Source = 0,
        //        IsUpdate = false,
        //        IsLoggedIn = true
        //    };
        //    QueueManager queueManager = new();
        //    var message = Newtonsoft.Json.JsonConvert.SerializeObject(appLogEvent);
        //    queueManager.SendDataInQueue("application.login.log.created.event", message, EnumBusConnection.Log);
        //}

        //private static void LogoutLog(string id)
        //{
        //    ApplicationLoginLogModel appLogEvent = new()
        //    {
        //        UserId = id,
        //        LogoutTime = DateTime.Now,
        //        IsUpdate = true
        //    };
        //    QueueManager queueManager = new();
        //    var message = Newtonsoft.Json.JsonConvert.SerializeObject(appLogEvent);
        //    queueManager.SendDataInQueue("application.login.log.created.event", message, EnumBusConnection.Log);
        //}

        #endregion "Login & Logout Log"
    }
}