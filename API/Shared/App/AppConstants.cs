using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.1.3";


        public const string ChangeLog = 
            """
            Рефакторинг логирования и трассировки, переход на OTEL

            - Удалены самописные классы и расширения для логирования/трассировки
            - Внедрён Telemetry с единым ActivitySource для всех слоёв
            - Добавлен кастомный консольный лог-форматтер с цветами
            - Везде используется стандартный ILogger и OTEL ActivitySource
            - Поддержка экспорта логов и трейсов в OTLP через .env
            - Изменена иерархия исключений, добавлен ConflictException
            - Удалено логирование SQL-запросов в репозиториях
            - Обновлены шаблоны .env, DI и конфигурация
            - Исправлены имена параметров маршрута в контроллерах
            - Удалены устаревшие утилиты для валидации и маппинга
            - Унифицирована структура ошибок в ответах API
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
