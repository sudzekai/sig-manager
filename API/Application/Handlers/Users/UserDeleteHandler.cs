using Contracts.Commands.Users;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users
{
    public class UserDeleteHandler(IUserRepository repository) : ICommandHandler<UserDeleteCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserDeleteCommand command)
        {
            _ = await repository.GetAsync(command.Id)
                ?? throw NotFoundException.UserWithId(command.Id);

            await repository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
