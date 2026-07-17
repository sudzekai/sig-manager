using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Commands.Users.Write;
using Contracts.Objects.Dtos.User;
using Domain.Models.Users;
using Domain.ValueObjects.Roles;
using Domain.ValueObjects.Users;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Users.Write
{
    public class UserCreateHandler(IUserRepository repository, IHashService hashService, IUserQuery query) : ICommandHandler<UserCreateCommand, UserInfoDto>
    {
        public async Task<UserInfoDto> HandleAsync(UserCreateCommand command)
        {
            var username = Username.FromValue(command.Dto.Username);

            if (await repository.GetIdByUsernameAsync(username) is not null)
                throw ConflictException.UserUsername();

            var email = UserEmail.FromValue(command.Dto.Email);

            if (await repository.GetIdByEmailAsync(email) is not null)
                throw ConflictException.UserEmail();

            var phoneNumber = UserPhoneNumber.FromValue(command.Dto.PhoneNumber);

            if (await repository.GetIdByPhoneNumberAsync(phoneNumber) is not null)
                throw ConflictException.UserPhoneNumber();

            string passwordHash = hashService.HashString(command.Dto.Password);

            User user = User.Create(
                username,
                UserFullName.FromValue(command.Dto.FullName),
                email,
                phoneNumber,
                UserPasswordHash.FromValue(passwordHash),
                RoleId.FromValue(1));

            var id = await repository.AddAsync(user);

            return await query.GetByIdAsync(id.Value)
                ?? throw NotFoundException.UserWithId(id.Value);
        }
    }
}
