using System.Diagnostics;

namespace Shared.OpenTelemetry.Tracing.Sources
{
    public static class ActivitySourceDictionary
    {
        public static class Sources
        {
            public static readonly string[] UserList = [
                Controllers.Users.Name,
                Services.Users.Name,
                Repositories.Users.Name
            ];

            public static readonly string[] ShiftList = [
                Controllers.Shifts.Name,
                Services.Shifts.Name,
                Repositories.Shifts.Name
            ];

            public static readonly string[] MiddlewareList =
            [
                Middlewares.Log.Name,
                Middlewares.Access.Name,
                Middlewares.Jwt.Name,
                Middlewares.Requests.Name
            ];
            public static readonly string[] FiltersList =
            [
                Filters.Response.Name,
                Filters.Exception.Name
            ];
        }

        public static class Middlewares
        {
            private const string source = "Middleware";

            public static readonly ActivitySource Log = new($"{source}.{nameof(Log)}");
            public static readonly ActivitySource Access = new($"{source}.{nameof(Access)}");
            public static readonly ActivitySource Jwt = new($"{source}.{nameof(Jwt)}");
            public static readonly ActivitySource Requests = new($"{source}.{nameof(Requests)}");
        }

        public static class Controllers
        {
            private const string source = "Controller";

            public static readonly ActivitySource CommandProcessor = new($"{source}.{nameof(CommandProcessor)}");
            public static readonly ActivitySource Users = new($"{source}.{nameof(Users)}");
            public static readonly ActivitySource Shifts = new($"{source}.{nameof(Shifts)}");
        }
        public static class Services
        {
            private const string source = "Service";

            public static readonly ActivitySource CommandProcessor = new($"{source}.{nameof(CommandProcessor)}");
            public static readonly ActivitySource Users = new($"{source}.{nameof(Users)}");
            public static readonly ActivitySource Shifts = new($"{source}.{nameof(Shifts)}");
        }

        public static class Repositories
        {
            private const string source = "Repository";

            public static readonly ActivitySource Users = new($"{source}.{nameof(Users)}");
            public static readonly ActivitySource Shifts = new($"{source}.{nameof(Shifts)}");
        }
        public static class Filters
        {
            private const string source = "Filter";

            public static readonly ActivitySource Exception = new($"{source}.{nameof(Exception)}");
            public static readonly ActivitySource Response = new($"{source}.{nameof(Response)}");
        }

    }
}
