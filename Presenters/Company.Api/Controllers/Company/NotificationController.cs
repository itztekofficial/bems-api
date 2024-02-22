using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middlewares;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Api.Controllers
{
    /// <summary>
    ///NotificationController
    /// </summary>
    public class NotificationController : BaseController
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// NotificationController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="notificationService"></param>
        public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _logger.LogInformation("ReportController Initialized");
        }

        /// <summary>
        /// Get All Notification
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllNotification")]     
        public async Task<ApiResponse<IEnumerable<NotificationResponse>>> GetAllNotification(NotificationRequest notificationRequest)
        {
            try
            {
                var result = await _notificationService.GetNotificationListAsync(notificationRequest);
                return new ApiResponse<IEnumerable<NotificationResponse>> ()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<NotificationResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }        
    }
}