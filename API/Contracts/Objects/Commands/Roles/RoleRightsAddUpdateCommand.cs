using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Roles;

namespace Contracts.Objects.Commands.Roles
{
    public record RoleRightsAddUpdateCommand(int Id, RoleRightsUpdateDto Dto) : ICommand<Unit>;
}
