using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Company
{
    /// <summary>
    /// Notification Service
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// NotificationService
        /// </summary>
        /// <param name="unitOfWork"></param>
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GetNotificationRequestListAsync
        /// </summary>
        /// <param name="notificationRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NotificationResponse>> GetNotificationListAsync(NotificationRequest notificationRequest)
        {
            return await _unitOfWork.Notifications.GetNotificationListAsync(notificationRequest);
        }
    }
}