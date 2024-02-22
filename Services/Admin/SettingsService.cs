using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Main.Services.Contracts.Admin;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Admin;

namespace Main.Services.Admin
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(ISettingsRepository settingsRepository, ILogger<SettingsService> logger)
        {
            _settingsRepository = settingsRepository;
            _logger = logger;
            _logger.Log(LogLevel.Information, "SettingsService => Initialized");
        }

        #region "usp_Settings"

        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public async Task<MyProfileResponse> GetUserDetailsById(int id)
        {
            try
            {
                return await _settingsRepository.GetUserDetailsById(id);
            }
            catch (Exception ex)
            {
                Log.WriteLog("SettingsService", "GetUserDetailsById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserPWDById
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public async Task<string> GetUserPWDById(int id)
        {
            try
            {
                return await _settingsRepository.GetUserPWDById(id);
            }
            catch (Exception ex)
            {
                Log.WriteLog("SettingsService", "GetUserPWDById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// UpdateUserPWDById
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserPWDById(PWDChangeRequest request)
        {
            try
            {
                return await _settingsRepository.UpdateUserPWDById(request);
            }
            catch (Exception ex)
            {
                Log.WriteLog("SettingsService", "UpdateUserPWDById", ex.Message);
                throw;
            }
        }

        #endregion "usp_Settings"
    }
}