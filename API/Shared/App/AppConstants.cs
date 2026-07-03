using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shared.App
{
    public static class AppConstants
    {
        public const string CurrentVersion = "v0.2.0";


        public const string ChangeLog =
            """
            Добавлена поддержка CRUD для машин (Car), улучшения User

            Реализованы сервисы, контроллер, репозиторий, декораторы и DTO для работы с сущностью "Машина" (Car), включая OpenTelemetry и логирование. 
            Добавлены фильтры по датам в GetCarsListRequest. 
            В DI-композицию включены сервисы и репозитории для машин. 

            Исправлено именование CarUpdateInfoDto -> CarInfoUpdateDto.
            Исправлены ошибки, из-за которых ендпоинты с параметрами запросов возвращали 400 даже при валидном вводе.
            Исправлены возвращаемые типы у методов репозитория для сущности Car.
            Исправлены сеттеры у свойств в CarCreateDto.

            Исправлена ошибка из-за которой при создании пользователя не проверялась уникальность номера телефона.
            Изменен запрос для фильтрации пользователей по поисковому запросу: теперь поиск идёт по первым символам во всех случаях.
            В модель сущности User добавлено новое свойство PhoneNumberLastFour, состоящее из 4 последних символов PhoneNumber, 
            для поиска по последним 4 цифрам номера телефона в запросе к БД.
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
