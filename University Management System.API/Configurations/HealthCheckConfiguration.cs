using ClassLibrary1University_Management_System.Infrastructure.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace University_Management_System.API.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static void AddHealthCheckServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(
                    connectionString: "Host=localhost;Database=postgres;Username=postgres;Password=mysequel1!",
                    healthQuery: "Select 1",
                    name: "DB Check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "sql", "npglsql", "healthchecks" })
                .AddCheck<ChuckNorrisHealthCheck>("Chuck Norris API");
        }

        public static void UseHealthServices(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(x => new
                        {
                            name = x.Key,
                            status = x.Value.Status.ToString(),
                            description = x.Value.Description
                        })
                    });
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}