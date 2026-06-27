using OpenTelemetry;
using OpenTelemetry.Logs;
using Shared.OpenTelemetry.Logging.Export;

namespace Shared.OpenTelemetry.Logging.Exporters
{
    public class LoggingConsoleExporter : BaseExporter<LogRecord>
    {
        public LoggingConsoleExporter(TextWriter console)
        {
            Console.SetOut(console);
        }

        public override ExportResult Export(in Batch<LogRecord> batch)
        {
            foreach (var record in batch)
            {
                var data = LogRecordExport.FromLogRecord(record);

                Console.Write($"{data.Time:[HH:mm:ss]}");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (data.TraceId != "00000000000000000000000000000000") 
                    Console.Write($" ID: {data.TraceId}");
                Console.ResetColor();

                Console.Write($" | ");

                Console.ForegroundColor = data.Level switch
                {
                    "Information" => ConsoleColor.Blue,
                    "Warning" => ConsoleColor.Yellow,
                    "Error" => ConsoleColor.Red,
                    _ => Console.ForegroundColor,
                };

                Console.Write($"{data.Level,-11}");
                Console.ResetColor();
                Console.Write($" | {data.Message}\n");

                if (data.Body is not null)
                {

                    Console.WriteLine($"{new string('-', 10)} body {new string('-', 10)}");
                    foreach (var item in data.Body)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{$"{item.Key}:",-20}");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"{item.Value}\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine(new string('-', 26));
                    Console.WriteLine();
                }
            }

            return ExportResult.Success;
        }
    }
}
