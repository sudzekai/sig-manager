using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.1.0";

        public const string ChangeLog = @"Изменения несовместимые с предыдущей версией решения
Рефакторинг архитектуры: слои, DI, пользователи, валидация

- Введены слои Contracts, Domain, CompositionRoot для разделения интерфейсов, бизнес-логики и регистрации зависимостей
- Сервисы Application переписаны под новые интерфейсы, добавлен UsersService с декоратором для логирования и трассировки
- Реализован репозиторий пользователей на MySQL, декоратор, конвертеры и расширения для IDataReader
- В Presentation добавлен UsersController с CRUD-методами, централизованная валидация моделей через ValidateModelState
- Переработаны фильтры ошибок и результатов, добавлен глобальный обработчик необработанных исключений
- В Shared реализованы базовые исключения, утилиты для логирования, маппинга и валидации
- DI и Program.cs переписаны для использования CompositionRoot
- Обновлены .gitignore, .csproj, структура решения
- Удалены устаревшие сервисы и классы, внедрена строгая типизация, улучшено логирование и трассировка
";

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
