using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Users.Update;
using Domain.ValueObjects.Users;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Users.Update
{
    public class UserInfoUpdateHandler(IUserRepository repository) : ICommandHandler<UserInfoUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserInfoUpdateCommand command)
        {
            var user = await repository.GetAsync(UserId.FromValue(command.Id))
                ?? throw NotFoundException.UserWithId(command.Id);

            if (command.Dto.Username != null)
            {
                var username = Username.FromValue(command.Dto.Username);

                if (await repository.GetIdByUsernameAsync(username) is not null)
                    throw ConflictException.UserEmail();

                user.Username = username;
            }

            if (command.Dto.FullName != null)
                user.FullName = UserFullName.FromValue(command.Dto.FullName);

            if (command.Dto.Email != null)
            {
                var email = UserEmail.FromValue(command.Dto.Email);

                if (await repository.GetIdByEmailAsync(email) is not null)
                    throw ConflictException.UserEmail();

                user.Email = email;
            }

            if (command.Dto.PhoneNumber != null)
            {
                var phoneNumber = UserPhoneNumber.FromValue(command.Dto.PhoneNumber);

                if (await repository.GetIdByPhoneNumberAsync(phoneNumber) is not null)
                    throw ConflictException.UserPhoneNumber();

                user.PhoneNumber = phoneNumber;
            }

            await repository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
