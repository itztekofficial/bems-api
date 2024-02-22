using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Middlewares;

namespace Admin.Api.Controllers
{
    /// <summary>
    ///SettingsController
    /// </summary>
    public class SettingsController : BaseController
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly ISettingsService _settingsService;

        /// <summary>
        /// SettingsController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="settingsService"></param>
        public SettingsController(ILogger<SettingsController> logger, ISettingsService settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
            _logger.LogInformation("SettingsController Initialized");
        }

        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetUserDetailsById")]
        public async Task<ApiResponse<MyProfileResponse>> GetUserDetailsById(ValueRequest request)
        {
            try
            {
                var result = await _settingsService.GetUserDetailsById(request.Id);
                return new ApiResponse<MyProfileResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<MyProfileResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetUserPWDById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetUserPWDById")]
        public async Task<ApiResponse<string>> GetUserPWDById(ValueRequest request)
        {
            try
            {
                var result = await _settingsService.GetUserPWDById(request.Id);
                return new ApiResponse<string>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<string>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// UpdateUserPWDById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateUserPWDById")]
        public async Task<ApiResponse<bool>> UpdateUserPWDById(PWDChangeRequest request)
        {
            try
            {
                var result = await _settingsService.UpdateUserPWDById(request);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }
    }
}