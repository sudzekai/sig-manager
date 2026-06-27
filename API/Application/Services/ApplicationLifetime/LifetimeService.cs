using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.OpenTelemetry.Logging.Extensions;

namespace Application.Services.ApplicationLifetime
{
    internal class LifetimeService : ILifetimeService
    {
        private readonly ILogger<LifetimeService> _logger;

        public LifetimeService(IHostApplicationLifetime lifetime, ILogger<LifetimeService> logger)
        {
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);
            lifetime.ApplicationStopped.Register(OnStopped);
            _logger = logger;
        }

        private void OnStarted()
        {
            _logger.CustomLogDebug("Приложение запущено");
        }

        private void OnStopping()
        {
            _logger.CustomLogDebug("Завершение работы...");
        }

        private void OnStopped()
        {
            _logger.CustomLogDebug("Приложение остановлено");
        }
    }
}
