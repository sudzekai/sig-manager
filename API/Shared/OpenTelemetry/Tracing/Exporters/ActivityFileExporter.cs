using OpenTelemetry;
using Shared.App;
using Shared.OpenTelemetry.Tracing.Export;
using System.Diagnostics;
using System.Text.Json;

namespace Shared.OpenTelemetry.Tracing.Exporters
{
    public class ActivityFileExporter : BaseExporter<Activity>
    {
        private readonly StreamWriter _stream;

        public ActivityFileExporter()
        {
            _stream = new StreamWriter(AppConstants.TraceLogFullPath, append: true);
        }

        public override ExportResult Export(in Batch<Activity> batch)
        {
            foreach (var activity in batch)
            {
                var data = ActivityRecordExport.FromActivity(activity);

                var json = JsonSerializer.Serialize(data, AppConstants.JsonOptionsDefault);

                _stream.WriteLine(json);
                _stream.Flush();
            }

            return ExportResult.Success;
        }
    }
}
