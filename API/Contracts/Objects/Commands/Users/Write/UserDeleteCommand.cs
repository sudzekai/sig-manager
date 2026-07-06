using Contracts.Interfaces.Application.Commands;

namespace Contracts.Objects.Commands.Users.Write
{
    public sealed record UserDeleteCommand(int Id) : ICommand<Unit>;
}
