using System.Diagnostics;

namespace Shared.OpenTelemetry.Tracing.Export
{
    internal class ActivityRecordExport
    {
        private ActivityRecordExport() { }

        public static ActivityRecordExport FromActivity(Activity activity)
        {
            return new()
            {
                TraceId = activity.TraceId.ToString(),
                SpanId = activity.SpanId.ToString(),
                ParentSpanId = activity.ParentSpanId.ToString(),
                Name = activity.Source.Name,
                OperationName = activity.OperationName,
                Kind = activity.Kind.ToString(),
                StartTime = activity.StartTimeUtc,
                Duration = activity.Duration,
                Tags = activity.Tags
            };
        }

        public string TraceId { get; set; }
        public string SpanId { get; set; }
        public string ParentSpanId { get; set; }
        public string Name { get; set; }
        public string OperationName { get; set; }
        public string Kind { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public object Tags { get; set; }
    }
}
