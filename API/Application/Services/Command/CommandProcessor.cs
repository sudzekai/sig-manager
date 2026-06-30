using Application.Services.Command.Handlers;
using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Models.Requests;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.OpenTelemetry.Logging.Extensions;
using Shared.OpenTelemetry.Tracing.Sources;
using Shared.Types.Exceptions;

namespace Application.Services.Command
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ILogger<CommandProcessor> _logger;

        private readonly IEnumerable<ICommandHandler> _handlers = [];

        public CommandProcessor(ILogger<CommandProcessor> logger, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _handlers = [
                new BanIpHandler(),
                new ShutDownHandler(applicationLifetime)
            ];
        }

        public async Task<string> ProcessAsync(CommandDto command)
        {
            using var activity = ActivitySourceDictionary.Services.CommandProcessor.StartActivity("Обработка команды");

            _logger.CustomLogInfo(
                "Получена команда",
                new() { ["command"] = command.Command }
            );

            var cmd = Objects.Command.Parse(command.Command);

            if (_handlers.FirstOrDefault(x => x.CommandName == cmd.Name) is var handler && handler is not null)
            {
                var result = await handler.HandleAsync(cmd.Args);

                _logger.CustomLogInfo(
                    "Команда выполнена"
                );

                return result;
            }

            throw new BadRequestException("команда не найдена");
        }
    }
}
