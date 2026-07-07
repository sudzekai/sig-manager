using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.2.3";

        public const string ChangeLog =
            """
            Рефакторинг DAL: новые репозитории смен, SQL-хелперы

            Добавлены репозитории и интерфейсы для CarShift, InfoShift, Shift, TicketShift с декораторами и логированием. 
            Предыдущая версия реализации IDbContext не позволяла в полной мере использовать транзакции в связи с чем была переписана: 
            - упразднены методы выполнения команд;
            - выполнение запросов осуществляется путем создания команды базы данных для полноценного контроля срока жизни команды и ее выполнения;
            - за счёт использования контекста БД в рамках запроса, а не единственного экземпляра на всё время работы приложения, 
              удалось обеспечить корректную работу соединений и транзакций. Теперь все обращения к БД в рамках одного запроса 
              выполняются через одно соединение, что позволяет использовать транзакции. Если во время выполнения возникает исключение, 
              транзакция остаётся незавершённой и при завершении жизненного цикла контекста автоматически откатывается.
            
            Добавлены методы-расширения для DbDataReader, старые - удалены.

            Введены SqlQuery-хелпер, большинство методов репозиториев используют методы хелпера для создания запросов
            
            Введены новые схемы таблиц.
            Введены новые классы-селекты для схем. 
            
            Большинство доменных моделей разделены на partial классы с различной логикой.
            Незначительные изменения в бизнес логике моделей.
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
