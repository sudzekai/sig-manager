using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Users.Update;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users.Update
{
    public class UserInfoUpdateHandler(IUserRepository repository) : ICommandHandler<UserInfoUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UserInfoUpdateCommand command)
        {
            var user = await repository.GetAsync(command.Id)
                ?? throw NotFoundException.UserWithId(command.Id);

            if (command.Dto.Username != null)
            {
                if (await IsUsernameExistsAsync(command.Dto.Username, command.Id))
                    throw ConflictException.UserUsername;

                user.ChangeUsername(command.Dto.Username);
            }

            if (command.Dto.FullName != null)
                user.ChangeFullName(command.Dto.FullName);

            if (command.Dto.Email != null)
            {
                if (await IsEmailExistsAsync(command.Dto.Email, command.Id))
                    throw ConflictException.UserEmail;

                user.ChangeEmail(command.Dto.Email);
            }

            if (command.Dto.PhoneNumber != null)
            {
                if (await IsPhoneNumberExistsAsync(command.Dto.PhoneNumber, command.Id))
                    throw ConflictException.UserPhoneNumber;

                user.ChangePhoneNumber(command.Dto.PhoneNumber);

            }

            await repository.UpdateAsync(user);

            return Unit.Value;
        }

        private async Task<bool> IsUsernameExistsAsync(string username, int excludedId)
            => await repository.GetIdByUsernameAsync(username) is var id && id is not null && id != excludedId;

        private async Task<bool> IsEmailExistsAsync(string email, int excludedId)
            => await repository.GetIdByEmailAsync(email) is var id && id is not null && id != excludedId;

        private async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int excludedId)
           => await repository.GetIdByPhoneNumberAsync(phoneNumber) is var id && id is not null && id != excludedId;
    }
}
