using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Presentation.Internal.Utilities.Logging
{
    internal class LogFormatter : ConsoleFormatter
    {
        public LogFormatter() : base("custom") { }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            var msg = logEntry.Formatter(logEntry.State, logEntry.Exception);

            var prefix = $"[{GetDarkGrayString(DateTime.Now.ToString("HH:mm:ss"))} | {GetFormattedLogLevel(logEntry.LogLevel.ToString())}]";

            textWriter.Write($"{prefix} {msg.Split("\n").First()}\n");

            foreach (var line in msg.Split("\n").Skip(1))
            {
                textWriter.Write($"{line.PadLeft(18 + line.Length)}\n");
            }

            if (logEntry.Exception is not null)
            {
                textWriter.WriteLine(logEntry.Exception);
            }
        }

        private static string GetFormattedLogLevel(string log) => log switch
        {
            "Information" => GetBlueString("INFO"),
            "Debug" => GetDarkGrayString("DBUG"),
            "Warning" => GetYellowString("WARN"),
            "Error" => GetRedString("ERR "),
            "Critical" => GetDarkRedString("CRIT"),
            _ => log
        };

        private static string GetRedString(string input)
        {
            return $"\u001b[31m{input}\u001b[0m";
        }

        private static string GetDarkRedString(string input)
        {
            return $"\u001b[31;1m{input}\u001b[0m";
        }

        private static string GetGreenString(string input)
        {
            return $"\u001b[32m{input}\u001b[0m";
        }

        private static string GetBlueString(string input)
        {
            return $"\u001b[34m{input}\u001b[0m";
        }

        private static string GetDarkGrayString(string input)
        {
            return $"\u001b[90m{input}\u001b[0m";
        }

        private static string GetYellowString(string input)
        {
            return $"\u001b[33m{input}\u001b[0m";
        }
    }
}
