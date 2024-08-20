using E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces;
using E_Learning_Platform_API.Infrastructure.Configurations;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies
{
    public class InstructorCreatedExamEmailStrategy(SendGridSettings sendGridSettings) : INotificationService
    {
        public async Task Send(string userName, string to, Dictionary<string, string> body)
        {
            var client = new SendGridClient(sendGridSettings.ApiKey);
            var from = new EmailAddress(sendGridSettings.FromEmail, "E-Learning System");
            var subject = "Exam Created !";
            var toUser = new EmailAddress(to);
            var htmlContent = $"<strong>{userName}, You have created exam for your course '{body["CourseName"]}'!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, toUser, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
