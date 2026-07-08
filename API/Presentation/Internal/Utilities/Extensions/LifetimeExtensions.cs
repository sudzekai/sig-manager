namespace Presentation.Internal.Utilities.Extensions
{
    internal static class LifetimeExtensions
    {
        public static void UseAppLifetimeLogging(this IHost app)
        {
            var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            lifetime.ApplicationStarted.Register(() =>
                logger.LogInformation("Приложение запущено"));

            lifetime.ApplicationStopping.Register(() =>
                logger.LogInformation("Завершение работы..."));

            lifetime.ApplicationStopped.Register(() =>
                logger.LogInformation("Приложение остановлено"));
        }
    }
}
