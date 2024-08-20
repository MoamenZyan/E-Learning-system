using E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces;

namespace E_Learning_Platform_API.Infrastructure.Services.NotificationServices
{
    public class NotificationContext
    {
        private INotificationService _notificationService = null!;
        public void SetNotificationServiceStrategy(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task SendNotification(string userName, string to, Dictionary<string, string> body)
        {
            await _notificationService.Send(userName, to, body);
        }
    }
}
