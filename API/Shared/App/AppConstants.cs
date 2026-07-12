using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.3.0";

        public const string ChangeLog =
            """
            Реализация CarShift, UserShift, Position и новых DTO

            - Добавлены интерфейсы и реализации репозиториев/запросов для CarShift, UserShift, Position, InfoShift, TicketShift
            - Реализованы декораторы для логирования и трассировки
            - Внедрён функционал открытия/закрытия смены машинок с валидацией и транзакциями
            - Расширены и переработаны DTO для CarShift, добавлены пользователи и позиции
            - Методы репозиториев теперь реализованы полностью
            - Добавлены команды и запросы для ролей и парков
            - В контроллере CarShiftsController добавлена валидация моделей
            - Исправлены ошибки в наименованиях и типах (CashLess → Cashless)
            - Добавлены новые исключения NotFoundException
            - Проведён рефакторинг: команды и запросы по ролям и паркам вынесены в отдельные пространства имён
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
