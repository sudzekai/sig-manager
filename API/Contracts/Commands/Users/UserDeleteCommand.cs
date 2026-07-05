using Contracts.Interfaces.Application.Commands;
using Contracts.Objects;

namespace Contracts.Commands.Users
{
    public sealed record UserDeleteCommand(int Id) : ICommandHandler<Unit>;
}
