using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace University_Management_System.API.Configurations
{
    public static class APIVersioningConfiguration
    {
        public static void AddApiVersioningServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiVersioningSettings = configuration.GetSection("ApiVersioning");

            services.AddApiVersioning(options =>
            {
                var defaultApiVersion = apiVersioningSettings.GetValue<string>("DefaultApiVersion").Split(".");
                options.DefaultApiVersion = new ApiVersion(
                    int.Parse(defaultApiVersion[0]),
                    int.Parse(defaultApiVersion[1])
                );
                options.AssumeDefaultVersionWhenUnspecified = apiVersioningSettings.GetValue<bool>("AssumeDefaultVersionWhenUnspecified");
                options.ReportApiVersions = apiVersioningSettings.GetValue<bool>("ReportApiVersions");

                var apiVersionReader = apiVersioningSettings.GetValue<string>("ApiVersionReader");
                if (apiVersionReader == "Header")
                {
                    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                }
                else if (apiVersionReader == "QueryString")
                {
                    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                }
                else
                {
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("api-version")
                    );
                }
            });
        }
    }
}