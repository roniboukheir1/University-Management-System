using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using University_Management_System.Application.Settings;

namespace University_Management_System.API.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            /*
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection(nameof(AuthenticationSettings)).Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);
            */

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:8080/realms/ums"; // Keycloak URL
                    options.Audience = "umsclient"; // Client ID from Keycloak
                    options.RequireHttpsMetadata = false; // Set to true in production

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false, 
                    };
                });
        }

        public static void UseAuthenticationServices(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}   