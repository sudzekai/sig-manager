using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Commands.Users.Update
{
    public sealed record UserRoleUpdateCommand(int Id, UserRoleUpdateDto Dto) : ICommand<Unit>;
}
