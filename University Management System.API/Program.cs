using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ClassLibrary1University_Management_System.Infrastructure.Services;
using University_Management_System.API.Configurations;
using University_Management_System.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.FileProviders;
using RabbitMQ.Client;
using University_Management_System.API.Settings;
using University_Management_System.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers()
    .AddOData(options => options.Select().Expand().Filter().OrderBy().Count().SetMaxTop(100));


var jwtSettings = new JWTSettings();
builder.Configuration.GetSection(nameof(JWTSettings)).Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<IModel>(provider =>
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    var connection = factory.CreateConnection();
    return connection.CreateModel();
});
builder.Services.AddScoped<CoursePublisher>();
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddHealthCheckServices(builder.Configuration);
builder.Services.AddLocalizationServices();
builder.Services.AddApiVersioningServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddHangfireServices(builder.Configuration);
builder.Services.AddCustomServicesAndRepositories(builder.Configuration);

string fileStorageConnectionString = builder.Configuration.GetConnectionString("AzureBlobStorage");
builder.Services.AddSingleton<IFileStorageService>(new AzureBlobStorageService(fileStorageConnectionString));

builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "profile_pictures")));

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization(LocalizationConfiguration.GetLocalizationOptions());
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions());
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

var scheduleJobs = app.Services.GetRequiredService<ScheduleJobs>();
scheduleJobs.Schedule();

app.UseAuthenticationServices();

app.Run();