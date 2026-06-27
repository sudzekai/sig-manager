using OpenTelemetry.Logs;

namespace Shared.OpenTelemetry.Logging.Export
{
    internal class LogRecordExport
    {
        private LogRecordExport() { }

        public static LogRecordExport FromLogRecord(LogRecord record)
        {
            var message = record.FormattedMessage;

            Dictionary<string, object?> attrs = new();

            record.ForEachScope<object?>((scope, _) =>
            {
                if (scope.Scope is Dictionary<string, object?> dict)
                    attrs = dict;
            }, null);

            return new LogRecordExport
            {
                Time = record.Timestamp,
                TraceId = record.TraceId.ToString(),
                Level = record.LogLevel.ToString(),
                Message = message,
                Body = attrs.Count > 0 ? attrs : null,
            };
        }

        public DateTime Time { get; set; }
        public string Level { get; set; }
        public string TraceId { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object?>? Body { get; set; }
    }
}
