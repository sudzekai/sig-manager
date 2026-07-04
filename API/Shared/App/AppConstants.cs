using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.2.1";


        public const string ChangeLog =
            """
            (WIP) Рефакторинг: разделение Query и Repository слоёв

            Введён Query-слой для выборки DTO, репозитории теперь работают только с доменными моделями.
            Добавлены интерфейсы и реализации для ролей и прав, новые DTO, обновлены сигнатуры методов, удалены устаревшие конвертеры. 
            Проведён рефакторинг моделей, улучшена обработка ошибок, логирование и телеметрия.

            Реализованы репозиторий и запросы для модели Role.
            Частые исключения вынесены в отдельные статические свойства и методы.
            Добавлены dto для role и right.
            Удалена неиспользующаяся ошибка DataUnchangedException.
            Удалены конвертеры доменных моделей, теперь вся логика преобразования запроса к бд в модель находится в соответствующих методах запросов.
            
            Изменена логика доступа к данным бд: ранее использовались методы проверяющие наличие имени колонки в таблице и null в самой ячейке,
            что было сделано во избежание ошибок при выполнении запросов для получения неполной записи об объекте.
            Теперь же, благодаря добавлению на слой Query данные методы более не требуются, в связи с чем были упразднены и введены новые:
            - расширения существующих методов, вычисляющие внутри себя ordinal колонки для упрощения.
            - методы для проверки ячеек таблиц на null.

            Изменена структура доменных моделей:
            - внедрены приватные сеттеры для метода создания модели которые не сравнивают текущее значение свойства с переданным,
            не затрагивают свойство UpdatedAt при его наличии и решают проблему с предупреждениями компилятора.
            - удалён публичный конструктор для создания модели, на его место внедрён метод Create.
            - рефакторинг.

            WIP: Репозитории для доменных моделей User и Car нуждаются в реализации интерфейсов, поэтому на данный момент не работают.
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
