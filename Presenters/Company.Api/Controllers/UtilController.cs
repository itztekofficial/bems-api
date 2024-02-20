using Core;
using Core.DataModel;
using Core.Models.QueueModel;
using Core.Models.Request;
using Core.Util;
using Core.ViewModel;
using Login.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Login.Api.Controllers
{
    /// <summary>
    /// UtilController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UtilController : ControllerBase
    {
        private readonly ILogger<UtilController> _logger;
        private readonly IUtilService _utilService;

        /// <summary>
        /// UtilController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="utilService"></param>
        public UtilController(ILogger<UtilController> logger, IUtilService utilService)
        {
            _logger = logger;
            _utilService = utilService;
            _logger.LogInformation("UtilController Initialized");
        }

        /// <summary>
        /// SendOTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("SendOTP")]
        public async Task<ApiResponse<bool>> SendOTP(OTPRequest request)
        {
            bool isSuccess = false;
            try
            {
                string rndotp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                string otp = (rndotp?.Length > 6) ? rndotp.Substring(rndotp.Length - 6) : rndotp ?? "654321";
                SMSCreatedModel smsCreatedEvent = new()
                {
                    MobileNumber = request.mobileNo,
                    SmsTemplate = "Dear User, Your OTP is " + otp + " . Please do not share the OTP with anyone. Have a good day!  Thank you, Marg Books"
                };

                var isOTPGenerated = SMSClient.sendSMSFromVduit(smsCreatedEvent);
                if (isOTPGenerated)
                {
                    Otp otpreq = new()
                    {
                        MobileNo = request.mobileNo,
                        MobileOTP = otp,
                        IsActive = true,
                        LastUpdated = DateTime.Now
                    };
                    isSuccess = _utilService.SaveOTP(otpreq).Result;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilController", "SendOTP", ex.Message);
            }
            return new ApiResponse<bool>()
            {
                Status = isSuccess ? EnumStatus.Success : EnumStatus.Error,
                Data = isSuccess
            };
        }

        /// <summary>
        /// ValidateOTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateOTP")]
        public async Task<ApiResponse<bool>> ValidateOTP(ValidateOTPRequest request)
        {
            bool isSuccess = false;
            try
            {
                isSuccess = _utilService.ValidateOTP(request).Result;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilController", "ValidateOTP", ex.Message);
            }
            return new ApiResponse<bool>()
            {
                Status = isSuccess ? EnumStatus.Success : EnumStatus.Error,
                Data = isSuccess
            };
        }
    }
}