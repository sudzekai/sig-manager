using Shared.OpenTelemetry.Logging.Extensions;
using Shared.OpenTelemetry.Tracing.Sources;

namespace Presentation.Api.Middlewares
{
    public class LogMiddleware : IMiddleware
    {
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(ILogger<LogMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using var activity = ActivitySourceDictionary.Middlewares.Log.StartActivity("Логирование запроса");

            var ip = context.Connection.RemoteIpAddress;

            if (ip is not null && ip.IsIPv4MappedToIPv6)
                ip = ip.MapToIPv4();

            _logger.CustomLogInfo("Получен запрос",
                new()
                {
                    ["request.ip"] = ip?.ToString(),
                    ["request.method"] = context.Request.Method,
                    ["request.path"] = context.Request.Path
                }
            );

            await next.Invoke(context);
        }
    }
}
