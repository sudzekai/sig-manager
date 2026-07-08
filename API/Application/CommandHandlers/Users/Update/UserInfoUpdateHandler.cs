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
                if (await IsUsernameExistsAsync(command.Dto.Username, command.Id))
                    throw ConflictException.UserUsername;

                user.Username = Username.FromValue(command.Dto.Username);
            }

            if (command.Dto.FullName != null)
                user.FullName = UserFullName.FromValue(command.Dto.FullName);

            if (command.Dto.Email != null)
            {
                if (await IsEmailExistsAsync(command.Dto.Email, command.Id))
                    throw ConflictException.UserEmail;

                user.Email = UserEmail.FromValue(command.Dto.Email);
            }

            if (command.Dto.PhoneNumber != null)
            {
                if (await IsPhoneNumberExistsAsync(command.Dto.PhoneNumber, command.Id))
                    throw ConflictException.UserPhoneNumber;

                user.PhoneNumber = UserPhoneNumber.FromValue(command.Dto.PhoneNumber);

            }

            await repository.UpdateAsync(user);

            return Unit.Value;
        }

        private async Task<bool> IsUsernameExistsAsync(string username, int excludedId)
            => await repository.GetIdByUsernameAsync(Username.FromValue(username)) is var id && id is not null && id.Value != excludedId;

        private async Task<bool> IsEmailExistsAsync(string email, int excludedId)
            => await repository.GetIdByEmailAsync(UserEmail.FromValue(email)) is var id && id is not null && id.Value != excludedId;

        private async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int excludedId)
           => await repository.GetIdByPhoneNumberAsync(UserPhoneNumber.FromValue(phoneNumber)) is var id && id is not null && id.Value != excludedId;
    }
}
