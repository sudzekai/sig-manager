using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.3.1";

        public const string ChangeLog =
            """
            Реализован метод получения списка смен машин
            """;

        public static TextWriter StandartWriter { get; set; } = Console.Out;

        public static readonly string BaseDirectory = AppContext.BaseDirectory;

        public static readonly string AppData = Path.Combine(BaseDirectory, "AppData");

        public static readonly string DotEnvFullPath = Path.Combine(AppData, ".env");

        public static readonly string BanIpListFullPath = Path.Combine(AppData, "banip.json");

        public static readonly string LogsDirectory = Path.Combine(BaseDirectory, AppData, "Logs");

        public static readonly string AppLogFullPath = Path.Combine(LogsDirectory, "app.log");

        public static readonly string TraceLogFullPath = Path.Combine(LogsDirectory, "trace.log");

        public static readonly JsonSerializerOptions JsonOptionsDefault = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static readonly JsonSerializerOptions JsonOptionsPrettyWrite = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static NotImplementedException TODO(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
            => new($"TODO:\nfile: {filePath}\nmethod: {memberName}\nline number: {lineNumber}");
    }
}
