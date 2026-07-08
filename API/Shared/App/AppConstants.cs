using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.2.3";

        public const string ChangeLog =
            """
            WIP: Рефакторинг: переход на value object-ы и новые сущности

            - Внедрены value object-ы для идентификаторов, имен, статусов и др.
            - Доменные модели переписаны с использованием value object-ов
            - Добавлены репозитории и декораторы для парков, должностей, прав, связей ролей и прав
            - Изменены сигнатуры методов репозиториев под value object-ы
            - Введён базовый класс DomainModelBase для поддержки INotifyPropertyChanged
            - Удалены устаревшие классы и файлы, обновлён DI
            - Подготовлена инфраструктура для расширения функционала
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
