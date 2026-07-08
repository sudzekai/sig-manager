using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Users.Update;
using Domain.ValueObjects.Roles;
using Domain.ValueObjects.Users;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Users.Update
{
    public class UserRoleUpdateHandler(IUserRepository repository) : ICommandHandler<UserRoleUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserRoleUpdateCommand command)
        {
            var user = await repository.GetAsync(UserId.FromValue(command.Id))
                ?? throw NotFoundException.UserWithId(command.Id);

            user.RoleId = RoleId.FromValue(command.Dto.RoleId);

            await repository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
