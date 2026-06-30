using DotNetEnv;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Presentation.Objects.Types.Exceptions;
using Shared.App;
using Shared.OpenTelemetry.Logging.Exporters;
using Shared.OpenTelemetry.Tracing.Sources;

namespace Presentation.Configuration
{
    public static class Configurator
    {
        public static void LoadEnv(this WebApplicationBuilder builder)
        {
            Env.Load(AppConstants.DotEnvFullPath);

            builder.Configuration.AddEnvironmentVariables();
        }

        public static void ConfigureAPI(this WebApplicationBuilder builder)
        {
            var urls = builder.Configuration["API_ALLOWED_HOSTS"]
                ?? throw new MissingEnvironmentalVariableException("API_ALLOWED_HOSTS");

            builder.WebHost.UseUrls(urls);
        }

        public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(r =>
                {
                    r.AddService(builder.Environment.ApplicationName);
                })
                .WithTracing(t =>
                {
                    t.AddSource(ActivitySourceDictionary.Sources.UserList);
                    t.AddSource(ActivitySourceDictionary.Sources.ShiftList);
                    t.AddSource(ActivitySourceDictionary.Sources.MiddlewareList);
                    t.AddSource(ActivitySourceDictionary.Sources.FiltersList);

                    //t.AddProcessor(
                    //    new SimpleActivityExportProcessor(
                    //        new TracingFileExporter("trace.log"))
                    //    );

                    t.SetSampler(new AlwaysOnSampler());
                })
                .WithLogging(l =>
                {
                    //l.AddProcessor(
                    //    new SimpleLogRecordExportProcessor(
                    //        new LoggingFileExporter("applog.log"))
                    //    );

                    l.AddProcessor(
                        new SimpleLogRecordExportProcessor(
                            new LoggingConsoleExporter(Console.Out))
                        );
                });

            builder.Services.Configure<OpenTelemetryLoggerOptions>(o =>
            {
                o.IncludeScopes = true;
                o.IncludeFormattedMessage = true;
            });
        }

        public static async Task ConfigureEnvironmentAsync()
        {
            if (!Directory.Exists(AppConstants.AppData))
                Directory.CreateDirectory(AppConstants.AppData);

            if (!Directory.Exists(AppConstants.LogsDirectory))
                Directory.CreateDirectory(AppConstants.LogsDirectory);

            if (!File.Exists(AppConstants.DotEnvFullPath))
                await File.WriteAllTextAsync(AppConstants.DotEnvFullPath,
@"API_PATH_BASE=/api

API_ALLOWED_HOSTS=http://*:8080

DB_HOST=localhost
DB_PORT=3306
DB_NAME=db
DB_USER=root
DB_PASSWORD=root");

            if (!File.Exists(AppConstants.BanIpListFullPath))
                await File.WriteAllTextAsync(AppConstants.BanIpListFullPath, "[]");

            if (!File.Exists(AppConstants.AppLogFullPath))
                await File.WriteAllTextAsync(AppConstants.AppLogFullPath, "");

            if (!File.Exists(AppConstants.TraceLogFullPath))
                await File.WriteAllTextAsync(AppConstants.TraceLogFullPath, "");
        }
    }
}
