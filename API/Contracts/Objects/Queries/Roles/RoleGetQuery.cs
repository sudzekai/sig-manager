using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.Roles;

namespace Contracts.Objects.Queries.Roles
{
    public record RoleGetQuery(int Id) : IQuery<RoleInfoDto>;
}
