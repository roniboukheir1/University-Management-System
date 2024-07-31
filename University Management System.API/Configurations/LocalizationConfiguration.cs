using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace University_Management_System.API.Configurations
{
    public static class LocalizationConfiguration
    {
        public static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
        }

        public static RequestLocalizationOptions GetLocalizationOptions()
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ar"),
                new CultureInfo("fr"),
            };

            return new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                }
            };
        }
    }
}