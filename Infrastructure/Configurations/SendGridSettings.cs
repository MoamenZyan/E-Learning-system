using SendGrid.Helpers.Mail;

namespace E_Learning_Platform_API.Infrastructure.Configurations
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
    }
}
