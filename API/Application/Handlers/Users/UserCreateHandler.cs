using Contracts.Commands.Users;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Shared.Objects.Dtos.User;
using Shared.Types.Exceptions;

namespace Application.Handlers.Users
{
    public class UserCreateHandler(IUserRepository repository, IHashService hashService, IUserQuery query) : ICommandHandler<UserCreateCommand, UserInfoDto>
    {
        public async Task<UserInfoDto> HandleAsync(UserCreateCommand command)
        {
            if (await IsUsernameExistsAsync(command.Dto.Username))
                throw ConflictException.UserUsername;

            if (await IsEmailExistsAsync(command.Dto.Email))
                throw ConflictException.UserEmail;

            if (await IsPhoneNumberExistsAsync(command.Dto.PhoneNumber))
                throw ConflictException.UserPhoneNumber;

            string passwordHash = hashService.HashString(command.Dto.Password);

            User user = User.Create(command.Dto.Username, command.Dto.FullName, command.Dto.Email, command.Dto.PhoneNumber, passwordHash, 1);

            var id = await repository.AddAsync(user);

            return await query.GetByIdAsync(id)
                ?? throw NotFoundException.UserWithId(id);
        }

        private async Task<bool> IsUsernameExistsAsync(string username)
            => await repository.GetIdByUsernameAsync(username) is not null;

        private async Task<bool> IsEmailExistsAsync(string email)
            => await repository.GetIdByEmailAsync(email) is not null;

        private async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber)
            => await repository.GetIdByPhoneNumberAsync(phoneNumber) is not null;
    }
}
