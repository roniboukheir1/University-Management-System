using University_Management_System.Application.Handlers.StudentHandlers;
using University_Management_System.Application.Handlers.TeacherHandlers;
using University_Management_System.Application.Repositories;
using University_Management_System.Application.Services;

namespace University_Management_System.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static void AddCustomServicesAndRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(Program).Assembly,
                    typeof(AddEnrollmentCommandHandler).Assembly,
                    typeof(GetStudentByIdHandler).Assembly,
                    typeof(AddSessionCommandHandler).Assembly
                );
            });
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}