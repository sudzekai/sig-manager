using Contracts.Commands.Users;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users
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
                if (await IsPhoneNumberExistsAsync(command.Dto.PhoneNumber))
                    throw ConflictException.UserPhoneNumber;

                user.ChangePhoneNumber(command.Dto.PhoneNumber);

            }

            await repository.UpdateAsync(user);

            return Unit.Value;
        }

        private async Task<bool> IsUsernameExistsAsync(string username, int? excludedId = null)
        {
            var id = await repository.GetIdByUsernameAsync(username);

            return id != null && (!excludedId.HasValue || id != excludedId.Value);
        }

        private async Task<bool> IsEmailExistsAsync(string email, int? excludedId = null)
        {
            var id = await repository.GetIdByEmailAsync(email);

            return id != null && (!excludedId.HasValue || id != excludedId.Value);
        }

        private async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int? excludedId = null)
        {
            var id = await repository.GetIdByPhoneNumberAsync(phoneNumber);

            return id != null && (!excludedId.HasValue || id != excludedId.Value);
        }
    }
}
