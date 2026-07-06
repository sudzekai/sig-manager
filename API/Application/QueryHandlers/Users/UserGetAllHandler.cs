using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.User;
using Contracts.Objects.Queries.Users;

namespace Application.QueryHandlers.Users
{
    public class UserGetAllHandler(IUserQuery query) : IQueryHandler<UserGetAllQuery, IReadOnlyList<UserSimpleDto>>
    {
        public async Task<IReadOnlyList<UserSimpleDto>> HandleAsync(UserGetAllQuery command)
            => await query.GetAllAsync(command.Request);
    }
}
