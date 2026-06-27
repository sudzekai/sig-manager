using OpenTelemetry;
using OpenTelemetry.Logs;
using Shared.App;
using Shared.OpenTelemetry.Logging.Export;
using System.Text.Json;

namespace Shared.OpenTelemetry.Logging.Exporters
{
    public class LoggingFileExporter : BaseExporter<LogRecord>
    {
        private readonly StreamWriter _stream;

        public LoggingFileExporter()
        {
            _stream = new StreamWriter(AppConstants.AppLogFullPath, append: true);
        }

        public override ExportResult Export(in Batch<LogRecord> batch)
        {
            foreach (var record in batch)
            {
                var data = LogRecordExport.FromLogRecord(record);

                var json = JsonSerializer.Serialize(data, AppConstants.JsonOptionsDefault);

                _stream.WriteLine(json);
                _stream.Flush();
            }

            return ExportResult.Success;
        }
    }
}
