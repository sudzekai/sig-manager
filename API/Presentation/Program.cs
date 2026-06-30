using CompositionRoot;
using Presentation.Api.Filters;
using Presentation.Api.Middlewares;
using Presentation.Configuration;
using Presentation.DI;
using Presentation.Utilities.ExceptionsHandler;
using Presentation.Utilities.Extensions;
using Scalar.AspNetCore;
using Shared.App;
using System.Text;
using System.Text.Json;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Configurator.ConfigureEnvironmentAsync();
            GlobalExceptionHandler.Register();

            var builder = WebApplication.CreateBuilder(args);

            // global

            builder.LoadEnv();
            builder.ConfigureAPI();
            builder.ConfigureOpenTelemetry();

            // infrastructure
            await builder.Services.AddSingletonDatabaseAsync(GetConnectionString(builder.Configuration));
            builder.Services.AddScopedRepositories();

            // application
            builder.Services.AddApplicationServices();

            // presentation
            builder.Services.AddScopedFilters();
            builder.Services.AddTransietMiddleware();

            builder.Services.AddControllers(o =>
            {
                o.Filters.Add<ExceptionsFilter>();
                o.Filters.Add<ResultFilter>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            var pathBase = builder.Configuration["API_PATH_BASE"]
                ?? throw new ArgumentNullException("API_PATH_BASE");

            app.UseAppLifetimeLogging();

            app.UsePathBase(pathBase);

            app.MapOpenApi();

            app.MapScalarApiReference(o =>
            {
                o.Title = "SiG Manager API";
            });

            app.UseMiddleware<LogMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            await app.StartAsync();

            await LogStartAsync(builder, app);

            await app.WaitForShutdownAsync();
        }

        private static string GetConnectionString(ConfigurationManager config)
        {
            StringBuilder connectionString = new();

            var hostname = config.TryGetString("DB_HOST");
            connectionString.Append($"Server={hostname};");

            var port = config.TryGetString("DB_PORT");
            connectionString.Append($"Port={port};");

            var database = config.TryGetString("DB_NAME");
            connectionString.Append($"Database={database};");

            var username = config.TryGetString("DB_USER");
            connectionString.Append($"Uid={username};");

            var password = config.TryGetString("DB_PASSWORD");
            connectionString.Append($"Pwd={password};");

            return connectionString.ToString();
        }

        private static async Task LogStartAsync(WebApplicationBuilder builder, WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            var http = new HttpClient();

            http.DefaultRequestHeaders.UserAgent.ParseAdd("sig-manager");

            var json = await http.GetStringAsync("https://api.github.com/repos/sudzekai/sig-manager/tags");

            var doc = JsonDocument.Parse(json);

            var latestTag = doc.RootElement
                .EnumerateArray()
                .FirstOrDefault()
                .GetProperty("name")
                .GetString();

            if (latestTag != AppConstants.CurrentVersion)
                logger.LogWarning("Актуальная версия: {version}", latestTag);

            logger.LogInformation("Версия: {version}", AppConstants.CurrentVersion);

            logger.LogInformation("Список изменений текущей версии: \n{changelog}", AppConstants.ChangeLog);

            logger.LogInformation("Среда: {environment}", builder.Environment.EnvironmentName);

            logger.LogInformation("Сервер прослушивает: \n{pad}- {host}", new string(' ', 27), string.Join($"\n{new string(' ', 27)}- ", app.Urls));

            logger.LogInformation("Базовый путь API: {pathBase}", app.Configuration["API_PATH_BASE"]);
        }
    }
}
