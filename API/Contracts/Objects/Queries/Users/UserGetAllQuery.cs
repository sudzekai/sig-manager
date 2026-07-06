using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Queries.Users
{
    public sealed record UserGetAllQuery(GetUsersListRequest Request) : IQuery<IReadOnlyList<UserSimpleDto>>;
}
