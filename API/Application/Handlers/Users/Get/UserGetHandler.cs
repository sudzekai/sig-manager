using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Commands.Users.Get;
using Contracts.Objects.Dtos.User;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users.Get
{
    public class UserGetHandler(IUserQuery query) : ICommandHandler<UserGetCommand, UserInfoDto>
    {
        public async Task<UserInfoDto> HandleAsync(UserGetCommand command)
            => await query.GetByIdAsync(command.Id) ?? throw NotFoundException.UserWithId(command.Id);
    }
}
