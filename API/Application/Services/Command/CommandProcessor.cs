using Application.Objects.Dtos;
using Application.Services.Command.Handlers;
using Application.Types.Exceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.OpenTelemetry.Logging.Extensions;
using Shared.OpenTelemetry.Tracing.Sources;

namespace Application.Services.Command
{
    internal class CommandProcessor : ICommandProcessor
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

            var cmd = Objects.Command.Parse(command.Command);

            if (_handlers.FirstOrDefault(x => x.CommandName == cmd.Name) is var handler && handler is not null)
            {
                var result = await handler.HandleAsync(cmd.Args);

                _logger.CustomLogInfo(
                    "Выполнена команда",
                    new() { ["command"] = command.Command }
                );

                return result;
            }

            throw new BadRequestException("команда не найдена");
        }
    }
}
