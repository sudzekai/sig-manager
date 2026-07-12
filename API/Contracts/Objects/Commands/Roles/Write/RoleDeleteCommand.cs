using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Roles;

namespace Contracts.Objects.Commands.Roles.Write
{
    public record RoleDeleteCommand(int Id) : ICommand<Unit>;
}
