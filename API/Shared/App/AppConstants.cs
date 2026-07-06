using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.2.2";


        public const string ChangeLog =
            """
            Рефакторинг: внедрение CQRS и Command Dispatcher

            Переведён проект на CQRS с использованием паттерна Command Handler/Dispatcher. 
            Контроллеры теперь используют ICommandDispatcher для обработки команд. 
            Добавлены отдельные обработчики команд для пользователей, машин и Bash-команд. 
            Модель Car и связанные DTO изменены: поле Number заменено на Id. 
            Реализованы новые команды и методы поиска по уникальным полям в репозиториях. 
            Удалены устаревшие сервисы и интерфейсы, обновлена DI-композиция и исключения. 
            Улучшена расширяемость и тестируемость архитектуры.
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
