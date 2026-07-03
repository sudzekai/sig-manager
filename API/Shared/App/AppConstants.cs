using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.1.4";


        public const string ChangeLog =
            """
            Расширена трассировка с помощью Activity и OTel

            Добавлена расширенная трассировка сервисов и репозиториев через Activity и OpenTelemetry: 
            - Внедрены новые методы StartServiceActivity/StartRepositoryActivity
            - Добавлены теги операций и результата, реализован enum DbOperation и ActivityExtensions. 
            - Улучшено логирование ошибок и результатов в фильтрах и middleware. 
            - Проведён рефакторинг для поддержки новых возможностей трассировки.
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
    }
}
