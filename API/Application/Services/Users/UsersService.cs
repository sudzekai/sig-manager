using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Domain.Models;
using Shared.Types.Exceptions;

namespace Application.Services.Users
{
    public class UsersService(IUserRepository repository, IHashService hashService, IUserQuery query) : IUsersService
    {
        public async Task<UserInfoDto> CreateAsync(UserCreateDto createDto)
        {
            if (await IsUsernameExistsAsync(createDto.Username))
                throw ConflictException.UserUsername;

            if (await IsEmailExistsAsync(createDto.Email))
                throw ConflictException.UserEmail;

            if (await IsPhoneNumberExistsAsync(createDto.PhoneNumber))
                throw ConflictException.UserPhoneNumber;

            string passwordHash = hashService.HashString(createDto.Password);

            User user = User.Create(createDto.Username, createDto.FullName, createDto.Email, createDto.PhoneNumber, passwordHash, 1);

            var id = await repository.AddAsync(user);

            return await query.GetByIdAsync(id)
                ?? throw NotFoundException.UserWithId(id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _ = await query.GetByIdAsync(id)
                ?? throw NotFoundException.UserWithId(id);

            await repository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request)
            => await query.GetAllAsync(request);

        public async Task<UserInfoDto> GetById(int id)
            => await query.GetByIdAsync(id) ?? throw NotFoundException.UserWithId(id);

        public async Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto)
        {
            var user = await repository.GetAsync(id)
                ?? throw NotFoundException.UserWithId(id);

            if (updateDto.Username != null)
            {
                if (await IsUsernameExistsAsync(updateDto.Username, id))
                    throw ConflictException.UserUsername;

                user.ChangeUsername(updateDto.Username);
            }

            if (updateDto.FullName != null)
                user.ChangeFullName(updateDto.FullName);

            if (updateDto.Email != null)
            {
                if (await IsEmailExistsAsync(updateDto.Email, id))
                    throw ConflictException.UserEmail;

                user.ChangeEmail(updateDto.Email);
            }

            if (updateDto.PhoneNumber != null)
            {
                if (await IsPhoneNumberExistsAsync(updateDto.PhoneNumber))
                    throw ConflictException.UserPhoneNumber;

                user.ChangePhoneNumber(updateDto.PhoneNumber);

            }

            await repository.UpdateAsync(user);
        }

        public async Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto)
        {
            var user = await repository.GetAsync(id)
                ?? throw NotFoundException.UserWithId(id);

            string passwordHash = hashService.HashString(updateDto.Password);

            user.ChangePasswordHash(passwordHash);

            await repository.UpdateAsync(user);
        }

        public async Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto)
        {
            var user = await repository.GetAsync(id)
                ?? throw NotFoundException.UserWithId(id);

            user.ChangeRoleId(updateDto.RoleId);

            await repository.UpdateAsync(user);
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
