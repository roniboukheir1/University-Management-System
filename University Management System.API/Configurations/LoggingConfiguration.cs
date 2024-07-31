using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using NpgsqlTypes;

namespace University_Management_System.API.Configurations
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
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
        }
    }
}