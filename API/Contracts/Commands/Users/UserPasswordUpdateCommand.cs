using Contracts.Interfaces.Application.Commands;
using Contracts.Objects;
using Shared.Objects.Dtos.User;

namespace Contracts.Commands.Users
{
    public sealed record UserPasswordUpdateCommand(int Id, UserPasswordUpdateDto Dto) : ICommandHandler<Unit>;
}
