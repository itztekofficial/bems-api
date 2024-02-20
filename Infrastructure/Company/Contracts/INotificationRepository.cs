using Core.Models.Request;
using Core.Models.Response;

namespace Company.Repositories.Contracts;
public interface INotificationRepository
{
     Task<IEnumerable<NotificationResponse>> GetNotificationListAsync(NotificationRequest notificationRequest);
}