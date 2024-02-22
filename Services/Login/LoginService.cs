using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Main.Services.Contracts.Login;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Omu.ValueInjecter;
using Repositories.Contracts.Login;

namespace Main.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginService> _logger;
        private readonly AppSettings _appSettings;

        public LoginService(ILoginRepository loginRepository, ILogger<LoginService> logger, AppSettings appSettings)
        {
            _loginRepository = loginRepository;
            _logger = logger;
            _appSettings = appSettings;
            _logger.LogInformation("LoginService Initialized");
        }

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<LoginResponse> GetUserDetails(LoginRequest loginModel)
        {
            LoginResponse loginResponse = new();
            try
            {
                var result = await _loginRepository.GetUserDetails(loginModel);
                if (result != null && result.Id > 0)
                {
                    loginResponse.InjectFrom(result);
                }
                return loginResponse;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
                return loginResponse;
            }
        }

        /// <summary>
        /// GetUserDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginDetailResponse> GetUserDetailById(LoginMultiUserRequest request)
        {
            LoginDetailResponse loginResponse = new();
            try
            {
                var result = await _loginRepository.GetUserDetailById(request);
                if (result != null && result.UserData != null)
                {
                    loginResponse.InjectFrom(result);
                    loginResponse.Token = GenerateJWTToken(result);
                }
                return loginResponse;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginRepository", "GetUserDetailById", ex.Message);
                return loginResponse;
            }
        }

        /// <summary>  
        /// GetUserDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ValidateUserMultiRoleResponse> ValidateUserDetailById(int id)
        {
            ValidateUserMultiRoleResponse validateUserMultiRoleResponse = new();
            try
            {
                var result = await _loginRepository.ValidateUserDetailById(id);
                if (result != null && result != null)
                {
                    validateUserMultiRoleResponse.InjectFrom(result);
                }
                return validateUserMultiRoleResponse;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginRepository", "ValidateUserDetailById", ex.Message);
                return validateUserMultiRoleResponse;
            }
        }

        /// <summary>
        /// GetUserDetailById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetUserDetailById(int id)
        {
            UserResponse userResponse = new();
            try
            {
                var result = await _loginRepository.GetUserDetailById(id);
                if (result != null && result != null)
                {
                    userResponse.InjectFrom(result);
                }
                return userResponse;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginRepository", "GetUserDetailById", ex.Message);
                return userResponse;
            }
        }

        /// <summary>
        /// GetUserDetailsByEmail
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        //public async Task<ForgotPasswordResponse> GetUserDetailsByEmail(string emailId)
        //{
        //    ForgotPasswordResponse response = new();
        //    try
        //    {
        //        response = await _loginRepository.GetUserDetailsByEmail(emailId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog("LoginRepository", "GetUserDetailsByEmail", ex.Message);
        //    }
        //    return response;
        //}

        /// <summary>
        /// LogoutUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> LogoutUser(int id)
        {
            try
            {
                return await _loginRepository.LogoutUser(id);
            }
            catch (Exception ex)
            {
                Log.WriteLog("LoginRepository", "GetUserDetails", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private string GenerateJWTToken(LoginDetailResponse response)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", response.UserData.Id.ToString()),
                    new Claim("companyId", response.CompanyData.Id.ToString()),
                    new Claim("entityId", response.UserData.EntityId),
                    new Claim("departmentId", response.UserData.DepartmentId),
                    new Claim("roleId", response.UserData.RoleId),
                    new Claim("userTypeId", response.UserData.UserType.ToString()),
                    new Claim("userName", response.UserData.Name),
                    new Claim(JwtRegisteredClaimNames.Sub, "itztek"),
                    new Claim(JwtRegisteredClaimNames.Email, "itztek@hotmail.com"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                Issuer = _appSettings.ValidIssuer,
                Audience = _appSettings.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        public Task<ForgotPasswordResponse> GetUserDetailsByEmail(string emailId)
        {
            throw new NotImplementedException();
        }
    }
}