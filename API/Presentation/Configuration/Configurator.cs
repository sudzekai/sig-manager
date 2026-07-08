using DotNetEnv;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Presentation.Internal.Utilities.Extensions;
using Presentation.Internal.Utilities.Logging;
using Shared.App;
using Shared.OpenTelemetry;

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
            var urls = builder.Configuration.TryGetString("API_ALLOWED_HOSTS");
            builder.WebHost.UseUrls(urls);
        }

        public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            builder.Logging.AddConsole(o =>
            {
                o.FormatterName = "custom";
            });

            builder.Services.AddLogging(logging =>
            {
                logging.AddConsoleFormatter<LogFormatter, LogFormatterOptions>();
            });

            var grpcOtlp = builder.Configuration.TryGetNullableString("OLTP_ENDPOINT");

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(r =>
                {
                    r.AddService(builder.Environment.ApplicationName);
                })
                .WithTracing(t =>
                {
                    t.AddSource(Telemetry.Controller.Name);
                    t.AddSource(Telemetry.Service.Name);
                    t.AddSource(Telemetry.Repository.Name);
                    t.AddSource(Telemetry.Middleware.Name);
                    t.AddSource(Telemetry.Filter.Name);

                    if (!string.IsNullOrWhiteSpace(grpcOtlp))
                        t.AddOtlpExporter(o =>
                        {
                            o.Endpoint = new Uri(grpcOtlp);
                        });

                    t.SetSampler(new AlwaysOnSampler());
                })
                .WithLogging(l =>
                {
                    if (!string.IsNullOrWhiteSpace(grpcOtlp))
                        l.AddOtlpExporter(o =>
                        {
                            o.Endpoint = new Uri(grpcOtlp);
                        });
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
                    """
                    # OLTP_ENDPOINT используется для трансферинга otel логов и трейсеров в OLTP систему. Если не указано, то логи не будут отправляться в OLTP систему.
                    OLTP_ENDPOINT=

                    # API_BASE_PATH используется для указания базового пути API. Обязательное поле.
                    API_BASE_PATH=/api

                    # API_ALLOWED_HOSTS используется для указания разрешенных хостов для API. Обязательное поле.
                    API_ALLOWED_HOSTS=http://*:8080

                    # DB_HOST используется для указания хоста базы данных. Обязательное поле.
                    DB_HOST=localhost

                    # DB_HOST используется для указания хоста базы данных. Обязательное поле.
                    DB_PORT=3306

                    # DB_NAME используется для указания имени базы данных. Обязательное поле.
                    DB_NAME=db

                    # DB_USER используется для указания имени пользователя базы данных. Обязательное поле.
                    DB_USER=root

                    # DB_USER используется для указания пароля пользователя базы данных. Обязательное поле.
                    DB_PASSWORD=root
                    """
                );

            if (!File.Exists(AppConstants.BanIpListFullPath))
                await File.WriteAllTextAsync(AppConstants.BanIpListFullPath, "[]");

            if (!File.Exists(AppConstants.AppLogFullPath))
                await File.WriteAllTextAsync(AppConstants.AppLogFullPath, "");

            if (!File.Exists(AppConstants.TraceLogFullPath))
                await File.WriteAllTextAsync(AppConstants.TraceLogFullPath, "");
        }
    }
}
