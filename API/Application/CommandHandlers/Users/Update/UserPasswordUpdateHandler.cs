using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Users.Update;
using Domain.ValueObjects.Users;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Users.Update
{
    public class UserPasswordUpdateHandler(IUserRepository repository, IHashService hashService) : ICommandHandler<UserPasswordUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserPasswordUpdateCommand command)
        {
            var user = await repository.GetAsync(UserId.FromValue(command.Id))
                ?? throw NotFoundException.UserWithId(command.Id);

            string passwordHash = hashService.HashString(command.Dto.Password);

            user.PasswordHash = UserPasswordHash.FromValue(passwordHash);

            await repository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
