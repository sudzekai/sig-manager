using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Commands.Users.Get;
using Contracts.Objects.Dtos.User;

namespace Application.Handlers.Users.Get
{
    public class UserGetAllHandler(IUserQuery query) : ICommandHandler<UserGetAllCommand, IReadOnlyList<UserSimpleDto>>
    {
        public async Task<IReadOnlyList<UserSimpleDto>> HandleAsync(UserGetAllCommand command)
            => await query.GetAllAsync(command.Request);
    }
}
