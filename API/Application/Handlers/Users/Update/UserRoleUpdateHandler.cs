using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Users.Update;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users.Update
{
    public class UserRoleUpdateHandler(IUserRepository repository) : ICommandHandler<UserRoleUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserRoleUpdateCommand command)
        {
            var user = await repository.GetAsync(command.Id)
                ?? throw NotFoundException.UserWithId(command.Id);

            user.ChangeRoleId(command.Dto.RoleId);

            await repository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
