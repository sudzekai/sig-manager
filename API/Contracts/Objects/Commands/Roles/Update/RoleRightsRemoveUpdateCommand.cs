using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Objects.Commands.Roles.Update
{
    public record RoleRightsRemoveUpdateCommand(int Id, RoleRightsUpdateDto Dto) : ICommand<Unit>;
}
