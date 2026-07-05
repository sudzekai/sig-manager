using Contracts.Interfaces.Application.Commands;
using Contracts.Objects;
using Shared.Objects.Dtos.User;

namespace Contracts.Commands.Users
{
    public sealed record UserRoleUpdateCommand(int Id, UserRoleUpdateDto Dto) : ICommandHandler<Unit>;
}
