using Core.DataModel;
using Core.Models.Request;
using Core.Util;
using Login.Repositories.Contracts;
using Login.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace Login.Services
{
    public class UtilService : IUtilService
    {
        private readonly IUtilRepository _utilRepository;
        private readonly ILogger<UtilService> _logger;

        public UtilService(IUtilRepository utilRepository, ILogger<UtilService> logger)
        {
            _utilRepository = utilRepository;
            _logger = logger;
        }

        /// <summary>
        /// SaveOTP
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        public async Task<bool> SaveOTP(Otp otp)
        {
            try
            {
                return await _utilRepository.SaveOTP(otp);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilService", "SaveOTP", ex.Message);
                return false;
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
                return await _utilRepository.ValidateOTP(request);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilService", "ValidateOTP", ex.Message);
                return false;
            }
        }
    }
}