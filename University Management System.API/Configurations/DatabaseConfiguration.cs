using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Infrastructure;

namespace University_Management_System.API.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UmsContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("University Management System.Persistence")
                ));
        }
    }
}