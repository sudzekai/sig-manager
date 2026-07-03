using Shared.App;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Shared.Extensions
{
    public static class ActivitySourceExtensions
    {
        public static Activity? StartRichActivity(
            this ActivitySource activitySource, 
            [CallerMemberName] string name = "", 
            [CallerFilePath] string path = "", 
            ActivityKind kind = ActivityKind.Internal)
        {
            var activity = activitySource.StartActivity(name, kind);
            activity?.SetOk();

            activity?.SetTag("activity.class", Path.GetFileNameWithoutExtension(path));
            activity?.SetTag("activity.source", activitySource.Name);

            return activity;
        }

        public static Activity? StartServiceActivity(
            this ActivitySource activitySource, 
            string entity,
            string operation,
            [CallerMemberName] string name = "", 
            [CallerFilePath] string path = "", 
            ActivityKind kind = ActivityKind.Internal)
        {
            var activity = activitySource.StartRichActivity(name, path, kind);

            activity?.SetTag("entity", entity);
            activity?.SetTag("operation", operation);

            return activity;
        }

        public static Activity? StartRepositoryActivity(
            this ActivitySource activitySource, 
            string entity,
            string operation,
            [CallerMemberName] string name = "", 
            [CallerFilePath] string path = "", 
            ActivityKind kind = ActivityKind.Internal)
        {
            var activity = activitySource.StartRichActivity(name, path, kind);

            activity?.SetTag("entity", entity);
            activity?.SetTag("operation", operation);

            return activity;
        }
    }
}
