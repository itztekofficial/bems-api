using Core.Models.Request;
using Core.Models.Response;

namespace Company.Services.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponse>> GetNotificationListAsync(NotificationRequest notificationRequest);
    }
}