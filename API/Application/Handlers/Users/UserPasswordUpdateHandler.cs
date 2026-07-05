using Contracts.Commands.Users;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users
{
    public class UserPasswordUpdateHandler(IUserRepository repository, IHashService hashService) : ICommandHandler<UserPasswordUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserPasswordUpdateCommand command)
        {
            var user = await repository.GetAsync(command.Id)
                ?? throw NotFoundException.UserWithId(command.Id);

            string passwordHash = hashService.HashString(command.Dto.Password);

            user.ChangePasswordHash(passwordHash);

            await repository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
