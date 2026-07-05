using Contracts.Commands.Users;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Shared.Objects.Dtos.User;

namespace Application.Handlers.Users
{
    public class UserGetAllHandler(IUserQuery query) : ICommandHandler<UserGetAllCommand, IReadOnlyList<UserSimpleDto>>
    {
        public async Task<IReadOnlyList<UserSimpleDto>> HandleAsync(UserGetAllCommand command)
            => await query.GetAllAsync(command.Request);
    }
}
