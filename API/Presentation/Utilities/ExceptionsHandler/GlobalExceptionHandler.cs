namespace Presentation.Utilities.ExceptionsHandler
{
    internal static class GlobalExceptionHandler
    {
        public static void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private static void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                LogCritical($"{ex.GetType().ToString().Split(".").Last()}: {ex.Message}");

            LogInfo("Приложение остановлено");
            Environment.Exit(-1);
        }

        private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            LogCritical($"{e.Exception.GetType().ToString().Split(".").Last()}: {e.Exception.Message}");
            e.SetObserved();

            LogInfo("Приложение остановлено");
            Environment.Exit(-1);
        }

        private static void LogCritical(string message)
        {
            Console.Write($"{DateTime.Now:[HH:mm:ss]}");

            Console.Write($" | ");

            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.Write($"{"Critical",-11}");
            Console.ResetColor();

            Console.Write($" | {message}\n");
        }

        private static void LogInfo(string message)
        {
            Console.Write($"{DateTime.Now:[HH:mm:ss]}");

            Console.Write($" | ");

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write($"{"Information",-11}");
            Console.ResetColor();

            Console.Write($" | {message}\n");
        }
    }
}
