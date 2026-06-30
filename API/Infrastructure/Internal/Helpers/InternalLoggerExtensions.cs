using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Shared.OpenTelemetry.Logging.Extensions;
using System.Runtime.CompilerServices;

namespace Infrastructure.Internal.Helpers
{
    internal static class InternalLoggerExtensions
    {
        public static void CustomLogDebugSqlExecution(this ILogger logger, 
            string query, MySqlParameter[] parameters,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
        {
            var body = LogBodyFormatter.FormatSqlExecution(query, parameters);

            logger.CustomLogDebug("Выполнение запроса к базе данных", body, methodName, filePath);
        }
    }
}
