using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Globalization;
using ClassLibrary1University_Management_System.Infrastructure.Services;
using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using NpgsqlTypes;
using University_Management_System.Application.Handlers.StudentHandlers;
using University_Management_System.Application.Handlers.TeacherHandlers;
using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;
using University_Management_System.Persistence;
using University_Management_System.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("DefaultConnection"), "Logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar)},
            {"TimeStamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"Exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"LogEvent", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb)}
        })
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar"),
    new CultureInfo("fr"),
};

// Load API versioning settings from configuration
var apiVersioningSettings = builder.Configuration.GetSection("ApiVersioning");

var smtpServer = "mail.smtp2go.com";
var smtpPort = 2525;
var smtpUser = "Roni";
var smtpPass = "B019771AA229464981195D71B1CBA057";

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

builder.Services.AddHealthChecks()
    .AddNpgSql(
        connectionString: "Host=localhost;Database=postgres;Username=postgres;Password=mysequel1!",
        healthQuery: "Select 1",
        name: "DB Check",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "sql", "npglsql", "healthchecks" })
    .AddCheck<ChuckNorrisHealthCheck>("Chuch Norris API");

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
    config.UseConsole();
});
builder.Services.AddHangfireServer();
builder.Services.AddSingleton<ScheduleJobs>();

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

app.MapHealthChecks("/health", new HealthCheckOptions());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure localization
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new AcceptLanguageHeaderRequestCultureProvider()
    }
};

app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

var scheduleJobs = app.Services.GetRequiredService<ScheduleJobs>();
scheduleJobs.Schedule();

app.Run();
