using Shared.Extensions;
using Shared.OpenTelemetry;
using System.Diagnostics;

namespace Presentation.Api.Middlewares
{
    public class LogMiddleware(ILogger<LogMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using var activity = Telemetry.Middleware.StartRichActivity("Logging");
            var stopwatch = Stopwatch.StartNew();
            
            var ip = context.Connection.RemoteIpAddress;

            if (ip is not null && ip.IsIPv4MappedToIPv6)
                ip = ip.MapToIPv4();

            logger.LogInformation("Получен запрос: {Method} -> {Path} от: {Ip}", context.Request.Method, context.Request.Path.Value, ip);

            await next.Invoke(context);

            stopwatch.Stop();

            logger.LogInformation("Ответ на запрос: {StatusCode} lat: {Duration}ms", context.Response.StatusCode, stopwatch.Elapsed.TotalMilliseconds);

            activity?.SetTag("response.code", context.Response.StatusCode);

            var hasError = context.Items.TryGetValue("error.occurred", out var err);

            var errorType = context.Items.TryGetValue("error.type", out var type)
                ? type?.ToString()
                : null;

            if (errorType != null)
            {
                if (errorType == "internal")
                    activity?.SetFailed();

                activity?.SetTag("error.type", type);
            }
        }
    }
}
