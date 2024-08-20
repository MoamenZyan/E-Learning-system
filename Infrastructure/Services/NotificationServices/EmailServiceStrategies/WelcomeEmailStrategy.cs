using E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces;
using E_Learning_Platform_API.Infrastructure.Configurations;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies
{
    // Welcome Email Notification
    public class WelcomeEmailStrategy(SendGridSettings sendGridSettings) : INotificationService
    {
        public async Task Send(string userName, string to, Dictionary<string, string> body)
        {
            var client = new SendGridClient(sendGridSettings.ApiKey);
            var from = new EmailAddress(sendGridSettings.FromEmail, "E-Learning System");
            var subject = "Welcome On Board!";
            var toUser = new EmailAddress(to);
            var htmlContent = $"<strong>Hello {userName}, Welcome to E-Learning System!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, toUser, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
