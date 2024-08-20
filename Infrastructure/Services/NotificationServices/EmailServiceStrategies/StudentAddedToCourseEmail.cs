using E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces;
using E_Learning_Platform_API.Infrastructure.Configurations;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies
{
    public class StudentAddedToCourseEmail(SendGridSettings sendGridSettings) : INotificationService
    {
        public async Task Send(string userName, string to, Dictionary<string, string> body)
        {
            var client = new SendGridClient(sendGridSettings.ApiKey);
            var from = new EmailAddress(sendGridSettings.FromEmail, "E-Learning System");
            var subject = "Enrolled To Course!";
            var toUser = new EmailAddress(to);
            var htmlContent = $"<strong>{userName}, You enrolled in {body["CourseName"]} Course!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, toUser, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
