using Application.Services.Command.Handlers;
using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;
using Shared.Types.Exceptions;

namespace Application.Services.Command
{
    public class CommandProcessor(ILogger<CommandProcessor> logger, IHostApplicationLifetime applicationLifetime) : ICommandProcessor
    {
        private readonly ILogger<CommandProcessor> _logger = logger;

        private readonly IEnumerable<ICommandHandler> _handlers = [
                new BanIpHandler(),
                new ShutDownHandler(applicationLifetime)
            ];

        public async Task<string> ProcessAsync(CommandDto command)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var cmdText = command.Command;
            _logger.LogInformation("Получена команда {Command}", cmdText);

            var cmd = Objects.Command.Parse(command.Command);

            if (_handlers.FirstOrDefault(x => x.CommandName == cmd.Name) is var handler && handler is not null)
            {
                var result = await handler.HandleAsync(cmd.Args);

                _logger.LogInformation("Команда выполнена");

                return result;
            }

            throw new BadRequestException("команда не найдена", $"command: {command.Command}");
        }
    }
}
