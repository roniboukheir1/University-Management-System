public class EmailNotificationJob
{
    private readonly IEmailService _emailService;

    public EmailNotificationJob(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task SendEmailNotificationAsync(string to, string subject, string body)
    {
        await _emailService.SendEmailAsync(to, subject, body);
    }
}