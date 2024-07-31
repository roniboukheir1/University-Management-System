using ClassLibrary1University_Management_System.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.Console;

namespace University_Management_System.API.Configurations
{
    public static class HangfireConfiguration
    {
        public static void AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection"));
                config.UseConsole();
            });
            services.AddHangfireServer();
            services.AddSingleton<ScheduleJobs>();
        }
    }
}