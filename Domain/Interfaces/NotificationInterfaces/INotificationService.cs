namespace E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces
{
    public interface INotificationService
    {
        Task Send(string userName, string to, Dictionary<string, string> body);
    }
}
