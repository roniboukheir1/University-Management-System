using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using University_Management_System.Application.Handlers.StudentHandlers;
using University_Management_System.Application.Handlers.TeacherHandlers;
using University_Management_System.Common.Repositories;
using University_Management_System.Persistence;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;
using University_Management_System.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Load API versioning settings from configuration
var apiVersioningSettings = builder.Configuration.GetSection("ApiVersioning");

builder.Services.AddControllers()
    .AddOData(options => options.Select().Expand().Filter().OrderBy().Count().SetMaxTop(100));

// Add API Versioning
builder.Services.AddApiVersioning(options =>
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "University Management System API", Version = "v1" });
});

// Add DbContext
builder.Services.AddDbContext<UmsContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("University Management System.Persistence")
    ));

// Add Caching
builder.Services.AddMemoryCache();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(AddEnrollmentCommandHandler).Assembly,
        typeof(GetStudentByIdHandler).Assembly,
        typeof(AddSessionCommandHandler).Assembly
    );
});
// Add Services and Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "profile_pictures")));

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();