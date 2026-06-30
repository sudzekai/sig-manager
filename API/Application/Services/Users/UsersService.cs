using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.User;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Types.Exceptions;

namespace Application.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUserRepository repository, ILogger<UsersService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserInfoDto> CreateAsync(UserCreateDto createDto)
        {
            if (await IsUsernameExistsAsync(createDto.Username))
                throw new BadRequestException("Пользователь с таким именем уже существует");

            if (await IsEmailExistsAsync(createDto.Email))
                throw new BadRequestException("Пользователь с такой электронной почтой уже существует");

            if (await IsPhoneNumberExistsAsync(createDto.PhoneNumber))
                throw new BadRequestException("Пользователь с таким номером телефона уже существует");

            User user = new(createDto.Username, createDto.FullName, createDto.Email, createDto.PhoneNumber, createDto.Password, 1);

            var id = await _repository.CreateAsync(user);

            var created = await _repository.GetByIdAsync(id);

            return new(created.Id, created.Username, created.FullName, created.Email, created.PhoneNumber);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!(await IsUserExistsAsync(id)))
                throw new NotFoundException("Пользователь с таким идентификатором не найден");

            await _repository.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            return [.. result.Select(x => new UserSimpleDto(x.Id, x.Username, x.FullName))];
        }

        public async Task<UserInfoDto> GetById(int id)
        {
            var user = await _repository.GetByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден");

            return new(user.Id, user.Username, user.FullName, user.Email, user.PhoneNumber);
        }

        public async Task<UserInfoDto> GetByUsernameAsync(string username)
        {
            var user = await _repository.GetByUsernameAsync(username) ??
                throw new NotFoundException("Пользователь с таким именем пользователя не найден");

            return new(user.Id, user.Username, user.FullName, user.Email, user.PhoneNumber);
        }

        public async Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto)
        {
            var user = await _repository.GetFullByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден");

            if (updateDto.Username != null)
                user.ChangeUsername(updateDto.Username);

            if (updateDto.FullName != null)
                user.ChangeFullName(updateDto.FullName);

            if (updateDto.Email != null)
                user.ChangeEmail(updateDto.Email);

            if (updateDto.PhoneNumber != null)
                user.ChangePhoneNumber(updateDto.PhoneNumber);


            await _repository.UpdateAsync(user);
        }

        public async Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto)
        {
            var user = await _repository.GetFullByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден");

            user.ChangePasswordHash(updateDto.Password);

            await _repository.UpdateAsync(user);
        }

        public async Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto)
        {
            var user = await _repository.GetFullByIdAsync(id) ??
                throw new NotFoundException("Пользователь с таким идентификатором не найден");

            user.ChangeRoleId(updateDto.RoleId);

            await _repository.UpdateAsync(user);
        }

        private async Task<bool> IsUserExistsAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null) return false;

            return true;
        }

        private async Task<bool> IsUsernameExistsAsync(string username, int? excludedId = null)
        {
            var existing = await _repository.GetByUsernameAsync(username);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }

        private async Task<bool> IsEmailExistsAsync(string email, int? excludedId = null)
        {
            var existing = await _repository.GetByEmailAsync(email);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }

        private async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int? excludedId = null)
        {
            var existing = await _repository.GetByPhoneNumberAsync(phoneNumber);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }
    }
}
