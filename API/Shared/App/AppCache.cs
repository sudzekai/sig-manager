using System.Diagnostics.Metrics;
using System.Text.Json;

namespace Shared.App
{
    public static class AppCache
    {
        public static class BanIp
        {
            public static List<string> List { get; private set; } = [];

            public static async Task LoadListAsync()
            {
                var json = await File.ReadAllTextAsync(AppConstants.BanIpListFullPath);

                List = JsonSerializer.Deserialize<List<string>>(json, AppConstants.JsonOptionsPrettyWrite) ?? [];
            }

            public static async Task AddIpAsync(string ip)
            {
                List.Add(ip);

                var json = JsonSerializer.Serialize(List, AppConstants.JsonOptionsPrettyWrite);

                await File.WriteAllTextAsync(AppConstants.BanIpListFullPath, json);
            }

            public static async Task RemoveIpAsync(string ip)
            {
                List.Remove(ip);

                var json = JsonSerializer.Serialize(List, AppConstants.JsonOptionsPrettyWrite);

                await File.WriteAllTextAsync(AppConstants.BanIpListFullPath, json);
            }
        }

        public static class Metrics
        {
            public static readonly Meter Meter = new("SiGManager.API");

            public static readonly Counter<int> Requests =
                Meter.CreateCounter<int>("requests_total");

            public static readonly Counter<int> Errors =
                Meter.CreateCounter<int>("errors_total");

            public static readonly Counter<int> Warnings =
                Meter.CreateCounter<int>("warnings_total");

            public static readonly DateTime StartTime = DateTime.UtcNow;

            public static readonly ObservableGauge<double> UptimeSeconds =
                Meter.CreateObservableGauge<double>(
                    "app_uptime_seconds",
                    () => (DateTime.UtcNow - StartTime).TotalSeconds
                );
        }
    }
}
