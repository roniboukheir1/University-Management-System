using Hangfire;

namespace ClassLibrary1University_Management_System.Infrastructure.Services;

public class ScheduleJobs
{
    private readonly IRecurringJobManager _recurringJobManager;

    public ScheduleJobs(IRecurringJobManager recurringJobManager)
    {
        _recurringJobManager = recurringJobManager;
    }

    public void Schedule()
    {
        _recurringJobManager.AddOrUpdate<EmailNotificationJob>(
            "email-job",
            job => job.SendEmailNotificationAsync("roni.boukheir@lau.edu", "Hello", "Hello"),
            Cron.Daily);
    }
}