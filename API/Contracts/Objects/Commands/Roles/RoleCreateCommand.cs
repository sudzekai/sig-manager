using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Roles;

namespace Contracts.Objects.Commands.Roles
{
    public record RoleCreateCommand(RoleCreateDto Dto) : ICommand<RoleInfoDto>;
}
