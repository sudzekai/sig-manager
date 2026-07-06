using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos;

namespace Contracts.Objects.Commands.Bash
{
    public record BashExecuteCommand(BashCommandDto Dto) : ICommand<string>;
}
