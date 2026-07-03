using Shared.Types.Enums;
using System.Diagnostics;

namespace Shared.Extensions
{
    public static class ActivityExtensions
    {
        public static void SetOk(this Activity? activity)
        {
            activity?.SetTag("result.success", true);
        }

        public static void SetFailed(this Activity? activity)
        {
            activity?.SetTag("result.success", false);
        }

        public static void SetSqlTag(this Activity? activity, DbOperation operation, int parametersCount)
        {
            activity?.SetTag("db.system", $"mysql");
            activity?.SetTag("db.operation", operation.ToString().ToLowerInvariant());
            activity?.SetTag("db.parameter_count", parametersCount);
        }
    }
}
