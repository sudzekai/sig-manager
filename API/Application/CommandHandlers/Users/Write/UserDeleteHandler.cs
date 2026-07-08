using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Users.Write;
using Domain.ValueObjects.Users;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Users.Write
{
    public class UserDeleteHandler(IUserRepository repository) : ICommandHandler<UserDeleteCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserDeleteCommand command)
        {
            var result = await repository.DeleteAsync(UserId.FromValue(command.Id));

            if (!result)
                throw NotFoundException.UserWithId(command.Id);

            return Unit.Value;
        }
    }
}
