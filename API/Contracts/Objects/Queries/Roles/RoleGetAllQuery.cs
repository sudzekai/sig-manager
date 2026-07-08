using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.Roles;

namespace Contracts.Objects.Queries.Roles
{
    public record RoleGetAllQuery(GetRolesListRequest Request) : IQuery<IReadOnlyList<RoleSimpleDto>>;
}
