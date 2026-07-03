using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Domain.Models;
using Shared.Types.Exceptions;

namespace Application.Services.Users
{
    public class UsersService(IUserRepository repository, IHashService hashService) : IUsersService
    {
        public async Task<UserInfoDto> CreateAsync(UserCreateDto createDto)
        {
            if (await IsUsernameExistsAsync(createDto.Username))
                throw new ConflictException("Пользователь с таким именем уже существует", $"username: {createDto.Username} exists");

            if (await IsEmailExistsAsync(createDto.Email))
                throw new ConflictException("Пользователь с такой электронной почтой уже существует", $"email: {createDto.Email} exists");

            if (await IsPhoneNumberExistsAsync(createDto.PhoneNumber))
                throw new ConflictException("Пользователь с таким номером телефона уже существует", $"phoneNumber: {createDto.PhoneNumber} exists");

            string passwordHash = hashService.HashString(createDto.Password);

            User user = new(createDto.Username, createDto.FullName, createDto.Email, createDto.PhoneNumber, passwordHash, 1);

            var id = await repository.CreateAsync(user);

            var created = await repository.GetInfoByIdAsync(id) ?? throw new InternalServerException("Внутренняя ошибка сервера", "internal error: created user was null");

            return new(created.Id, created.Username, created.FullName, created.Email, created.PhoneNumber);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!(await IsUserExistsAsync(id)))
                throw new NotFoundException("Пользователь с таким идентификатором не найден", $"id: {id} doesn't exist");

            await repository.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request)
        {
            var result = await repository.GetAllAsync(request);

            return [.. result.Select(x => new UserSimpleDto(x.Id, x.Username, x.FullName))];
        }

        public async Task<UserInfoDto> GetById(int id)
        {
            var user = await repository.GetInfoByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден", $"id: {id} doesn't exist");

            return new(user.Id, user.Username, user.FullName, user.Email, user.PhoneNumber);
        }

        public async Task<UserInfoDto> GetByUsernameAsync(string username)
        {
            var user = await repository.GetInfoByUsernameAsync(username) ??
                throw new NotFoundException("Пользователь с таким именем пользователя не найден", $"username: {username} doesn't exist");


            return new(user.Id, user.Username, user.FullName, user.Email, user.PhoneNumber);
        }

        public async Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto)
        {
            var user = await repository.GetFullByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден", $"id: {id} doesn't exist");

            if (updateDto.Username != null)
            {
                if (await IsUsernameExistsAsync(updateDto.Username, id))
                    throw new ConflictException("Пользователь с таким именем уже существует", $"username: {updateDto.Username} exists");

                user.ChangeUsername(updateDto.Username);
            }

            if (updateDto.FullName != null)
                user.ChangeFullName(updateDto.FullName);

            if (updateDto.Email != null)
            {
                if (await IsEmailExistsAsync(updateDto.Email, id))
                    throw new ConflictException("Пользователь с такой электронной почтой уже существует", $"email: {updateDto.Email} exists");

                user.ChangeEmail(updateDto.Email);
            }

            if (updateDto.PhoneNumber != null)
            {
                if (await IsPhoneNumberExistsAsync(updateDto.PhoneNumber))
                    throw new ConflictException("Пользователь с таким номером телефона уже существует", $"phoneNumber: {updateDto.PhoneNumber} exists");

                user.ChangePhoneNumber(updateDto.PhoneNumber);

            }

            await repository.UpdateAsync(user);
        }

        public async Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto)
        {
            var user = await repository.GetFullByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден", $"id: {id} doesn't exist");

            string passwordHash = hashService.HashString(updateDto.Password);

            user.ChangePasswordHash(passwordHash);

            await repository.UpdateAsync(user);
        }

        public async Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto)
        {
            var user = await repository.GetFullByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден", $"id: {id} doesn't exist");

            user.ChangeRoleId(updateDto.RoleId);

            await repository.UpdateAsync(user);
        }

        private async Task<bool> IsUserExistsAsync(int id)
        {
            var existing = await repository.GetInfoByIdAsync(id);

            if (existing == null) return false;

            return true;
        }

        private async Task<bool> IsUsernameExistsAsync(string username, int? excludedId = null)
        {
            var existing = await repository.GetInfoByUsernameAsync(username);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }

        private async Task<bool> IsEmailExistsAsync(string email, int? excludedId = null)
        {
            var existing = await repository.GetInfoByEmailAsync(email);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }

        private async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int? excludedId = null)
        {
            var existing = await repository.GetInfoByPhoneNumberAsync(phoneNumber);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }
    }
}
