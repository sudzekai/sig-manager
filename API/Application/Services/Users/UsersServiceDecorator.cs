using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.User;
using Microsoft.Extensions.Logging;
using Shared.OpenTelemetry.Logging.Extensions;
using Shared.OpenTelemetry.Tracing.Sources;
using Shared.Utilities.Extensions;
using System.Diagnostics;

namespace Application.Services.Users
{
    public class UsersServiceDecorator : IUsersService
    {
        private readonly IUsersService _inner;
        private readonly ILogger<UsersService> _logger;
        private readonly ActivitySource _activitySource = ActivitySourceDictionary.Services.Users;

        public UsersServiceDecorator(IUsersService inner, ILogger<UsersService> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<UserInfoDto> CreateAsync(UserCreateDto createDto)
        {
            using var activity = _activitySource.StartActivity(nameof(CreateAsync));

            var result = await _inner.CreateAsync(createDto);

            _logger.CustomLogDebug("Создан пользователь", result.ToLogBody("created"));

            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(DeleteByIdAsync));

            await _inner.DeleteByIdAsync(id);

            _logger.CustomLogDebug($"Пользователь с id: {id} удалён");
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync()
        {
            using var activity = _activitySource.StartActivity(nameof(GetAllAsync));

            var result = await _inner.GetAllAsync();

            _logger.CustomLogDebug($"Получен список пользователей", new() { ["count"] = result.Count });

            return result;
        }

        public async Task<UserInfoDto> GetById(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetById));

            var result = await _inner.GetById(id);

            _logger.CustomLogDebug($"Получен пользователь", result.ToLogBody("user"));

            return result;
        }

        public async Task<UserInfoDto> GetByUsernameAsync(string username)
        {
            using var activity = _activitySource.StartActivity(nameof(GetByUsernameAsync));

            var result = await _inner.GetByUsernameAsync(username);

            _logger.CustomLogDebug($"Получен пользователь", result.ToLogBody("user"));

            return result;
        }

        public async Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto)
        {
            using var activity = _activitySource.StartActivity(nameof(UpdateInfoByIdAsync));

            await _inner.UpdateInfoByIdAsync(id, updateDto);

            _logger.CustomLogDebug($"Обновлена информация о пользователе с id: {id}", updateDto.ToLogBody("updated"));
        }

        public async Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto)
        {
            using var activity = _activitySource.StartActivity(nameof(UpdatePasswordByIdAsync));

            await _inner.UpdatePasswordByIdAsync(id, updateDto);

            _logger.CustomLogDebug($"Обновлён пароль пользователя с id: {id}");
        }

        public async Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto)
        {
            using var activity = _activitySource.StartActivity(nameof(UpdateRoleByIdAsync));

            await _inner.UpdateRoleByIdAsync(id, updateDto);

            _logger.CustomLogDebug($"Обновлена роль пользователя с id: {id}", updateDto.ToLogBody("updated"));
        }
    }
}
