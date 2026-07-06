using Application.CommandHandlers.Bash.InnerHandlers;
using Application.CommandHandlers.Bash.Interfaces;
using Application.CommandHandlers.Bash.Objects;
using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Commands.Bash;
using Microsoft.Extensions.Hosting;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Bash
{
    public class BashCommandHandler(IHostApplicationLifetime hostApplicationLifetime) : ICommandHandler<BashExecuteCommand, string>
    {
        private readonly IEnumerable<IBashCommandHandler> _handlers = [
                new BashBanIpHandler(),
                new BashExitHandler(hostApplicationLifetime)
            ];

        public async Task<string> HandleAsync(BashExecuteCommand command)
        {
            var cmdText = command.Dto.Command;

            var cmd = Command.Parse(command.Dto.Command);

            if (_handlers.FirstOrDefault(x => x.CommandName == cmd.Name) is var handler && handler is not null)
            {
                var result = await handler.HandleAsync(cmd.Args);

                return result;
            }

            throw new BadRequestException("команда не найдена", $"command: {command.Dto.Command} not found");
        }
    }
}
