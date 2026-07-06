using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Queries.Users
{
    public sealed record UserGetQuery(int Id) : IQuery<UserInfoDto>;
}
