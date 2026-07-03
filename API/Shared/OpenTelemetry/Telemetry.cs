using System.Diagnostics;

namespace Shared.OpenTelemetry
{
    public class Telemetry
    {
        public static readonly ActivitySource Controller = new("Controller");
        public static readonly ActivitySource Service = new("Service");
        public static readonly ActivitySource Repository = new("Repository");
        public static readonly ActivitySource Middleware = new("Middleware");
        public static readonly ActivitySource Filter = new("Filter");
    }
}
