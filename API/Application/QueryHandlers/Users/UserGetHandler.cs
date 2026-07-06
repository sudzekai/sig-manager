using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.User;
using Contracts.Objects.Queries.Users;
using Shared.Types.Exceptions;

namespace Application.QueryHandlers.Users
{
    public class UserGetHandler(IUserQuery query) : IQueryHandler<UserGetQuery, UserInfoDto>
    {
        public async Task<UserInfoDto> HandleAsync(UserGetQuery command)
            => await query.GetByIdAsync(command.Id) ?? throw NotFoundException.UserWithId(command.Id);
    }
}
