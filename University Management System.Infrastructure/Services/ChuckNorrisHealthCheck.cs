using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace ClassLibrary1University_Management_System.Infrastructure.Services;

public class ChuckNorrisHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var url = "https://chuck-norris-jokes.p.rapidapi.com/de/jokes/random";
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://chuck-norris-jokes.p.rapidapi.com/de/jokes/random"),
            Headers =
            {
                { "x-rapidapi-key", "cdf5683e04msh31efdaae01afc48p18e7a5jsn5aed37450fb5" },
                { "x-rapidapi-host", "chuck-norris-jokes.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }
            return HealthCheckResult.Unhealthy();
        }
        
    }
}