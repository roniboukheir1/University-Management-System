using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class EmailNotificationController : ControllerBase
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public EmailNotificationController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("enqueue")]
    public IActionResult EnqueueEmailNotification(string to, string subject, string body)
    {
        _backgroundJobClient.Enqueue<EmailNotificationJob>(job => job.SendEmailNotificationAsync(to, subject, body));
        return Ok("Email job enqueued.");
    }
}