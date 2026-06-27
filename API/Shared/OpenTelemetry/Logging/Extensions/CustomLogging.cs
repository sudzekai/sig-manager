using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Shared.OpenTelemetry.Logging.Extensions
{
    public static class CustomLogging
    {
        public static void CustomLogDebug(this ILogger logger,
            string message,
            Dictionary<string, object?>? data = null,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
            => logger.CustomLog(LogLevel.Debug, message, data, methodName, filePath);

        public static void CustomLogInfo(this ILogger logger,
            string message,
            Dictionary<string, object?>? data = null,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
            => logger.CustomLog(LogLevel.Information, message, data, methodName, filePath);

        public static void CustomLogWarn(this ILogger logger,
            string message,
            Dictionary<string, object?>? data = null,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
            => logger.CustomLog(LogLevel.Warning, message, data, methodName, filePath);

        public static void CustomLogError(this ILogger logger,
            string message,
            Dictionary<string, object?>? data = null,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
            => logger.CustomLog(LogLevel.Error, message, data, methodName, filePath);

        public static void CustomLogCritical(this ILogger logger,
            string message,
            Dictionary<string, object?>? data = null,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
            => logger.CustomLog(LogLevel.Critical, message, data, methodName, filePath);

        public static void CustomLog(this ILogger logger,
            LogLevel level,
            string message,
            Dictionary<string, object?>? data = null,
            string methodName = "",
            string filePath = "")
        {
            if (!logger.IsEnabled(level))
                return;

            string className = Path.GetFileNameWithoutExtension(filePath);

            var scope = new Dictionary<string, object>
            {
                ["caller.class"] = className,
                ["caller.method"] = methodName,
            };

            if (data != null)
                foreach (var item in data)
                    scope.TryAdd(item.Key, item.Value);

            using (logger.BeginScope(scope))
            {
                logger.Log(level, "{message}", message);
            }
        }
    }
}
