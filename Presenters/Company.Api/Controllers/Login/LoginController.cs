using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Login.Api.Controllers
{
    /// <summary>
    /// Login Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private ILoginService _loginService;

        /// <summary>
        /// LoginController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="loginService"></param>
        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
            _logger.LogInformation("LoginController Initialized");
        }

        /// <summary>
        /// ValidateUser
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns>ApiResponse</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateUser")]
        public async Task<ApiResponse<LoginResponse>> ValidateUser(LoginRequest loginRequestModel)
        {
            try
            {
                var result = await _loginService.GetUserDetails(loginRequestModel);
                return new ApiResponse<LoginResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "ValidateUser", ex.Message);
                return new ApiResponse<LoginResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// ValidateUserMultiRoleDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateUserMultiRoleDetailById")]
        public async Task<ApiResponse<ValidateUserMultiRoleResponse>> GetValidateUserDetailById(ValueRequest request)
        {
            try
            {
                var result = await _loginService.ValidateUserDetailById(request.Id);
                return new ApiResponse<ValidateUserMultiRoleResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "GetValidateUserDetailById", ex.Message);
                return new ApiResponse<ValidateUserMultiRoleResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetUserDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("GetUserDetailById")]
        public async Task<ApiResponse<LoginDetailResponse>> GetUserDetailById(LoginMultiUserRequest request)
        {
            try
            {
                var result = await _loginService.GetUserDetailById(request);
                return new ApiResponse<LoginDetailResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "GetUserDetailById", ex.Message);
                return new ApiResponse<LoginDetailResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetUserDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("GetUserDetailsById")]
        public async Task<ApiResponse<UserResponse>> GetUserDetailsById(ValueRequest request)
        {
            try
            {
                var result = await _loginService.GetUserDetailById(request.Id);
                return new ApiResponse<UserResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "GetValidateUserDetailById", ex.Message);
                return new ApiResponse<UserResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetUserDetailsByEmail
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("GetUserDetailsByEmail")]
        public async Task<ApiResponse<ForgotPasswordResponse>> GetUserDetailsByEmail(ValueRequestString request)
        {
            try
            {
                var result = await _loginService.GetUserDetailsByEmail(request.Id);
                return new ApiResponse<ForgotPasswordResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "GetUserDetailsByEmail", ex.Message);
                return new ApiResponse<ForgotPasswordResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// SendDetailToUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("SendDetailToUser")]
        public async Task<ApiResponse<bool>> SendDetailToUser(MailRequest request)
        {
            try
            {
                MailSMS mail = new();
                EmailHtmlBodyData emaildata = new()
                {
                    Header = "",
                    WelcomeMessage = "Dear " + request.UsrName + " ,",
                    MailBodyMessage = "Your AKUMS (CLM) password is <b>" + request.UsrPwd + "</b>, Please login your crediantials.<br /><br /> Note: Please do not share your security code and password to any one.",
                    IdMessage = "",
                    IdNumber = ""
                };
                //string htmldata = mail.CrateEmailHTMLTemplate(emaildata);
                //mail.SendEMail(request.EmailId, "Password received", htmldata.ToString(), string.Empty, null);

                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "SendDetailToUser", ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// LogoutUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("LogoutUser")]
        public async Task<ApiResponse<bool>> LogoutUser(ValueRequest request)
        {
            bool isSuccess = false;
            try
            {
                var result = await _loginService.LogoutUser(request.Id);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginController", "LogoutUser", ex.Message);
            }

            return new ApiResponse<bool>()
            {
                Status = isSuccess ? EnumStatus.Success : EnumStatus.Error,
                Data = isSuccess
            };
        }

        /// <summary>
        /// Get PWD (This should delete after live)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("GetUserPWD")]
        public async Task<ApiResponse<string>> GetUserPWD(ValueRequestString request)
        {
            string pwdstr = EncryptDecrypt.Decrypt(request.Id);
            return new ApiResponse<string>()
            {
                Status = EnumStatus.Success,
                Data = pwdstr
            };
        }
    }
}