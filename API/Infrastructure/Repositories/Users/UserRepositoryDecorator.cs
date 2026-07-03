using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Users
{
    public class UserRepositoryDecorator(IUserRepository inner, ILogger<UserRepository> logger) : IUserRepository
    {
        private readonly IUserRepository _inner = inner;
        private readonly ILogger<UserRepository> _logger = logger;

        public async Task<IReadOnlyList<User>> GetAllAsync(GetUsersListRequest request)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_all");
            _logger.LogInformation("Получение списка пользователей из БД");
            
            var result = await _inner.GetAllAsync(request);

            return result;
        }

        public async Task<User?> GetInfoByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_info_by_id");
            _logger.LogInformation("Получение информации о пользователе с id {id}", id);

            var result = await _inner.GetInfoByIdAsync(id);

            return result;
        }

        public async Task<User?> GetInfoByUsernameAsync(string username)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_info_by_username");
            _logger.LogInformation("Получение информации о пользователе с username {username}", username);

            var result = await _inner.GetInfoByUsernameAsync(username);

            return result;
        }

        public async Task<int> CreateAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "create");
            _logger.LogInformation("Создание записи о пользователе");

            var result = await _inner.CreateAsync(user);

            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "delete_by_id");
            _logger.LogInformation("Удаление записи о пользователе с id {id}", id);

            await _inner.DeleteByIdAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "update");
            _logger.LogInformation("Обновление записи о пользователе c id {id}", user.Id);

            await _inner.UpdateAsync(user);
        }

        public async Task<User?> GetInfoByEmailAsync(string email)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_info_by_email");
            _logger.LogInformation("Получение информации о пользователе с email {email}", email);

            var result = await _inner.GetInfoByEmailAsync(email);
            
            return result;
        }

        public async Task<User?> GetInfoByPhoneNumberAsync(string phoneNumber)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_info_by_phone_number");
            _logger.LogInformation("Получение информации о пользователе с phoneNumber {phoneNumber}", phoneNumber);

            var result = await _inner.GetInfoByEmailAsync(phoneNumber);

            return result;
        }

        public async Task<User?> GetFullByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_full_by_id");
            _logger.LogInformation("Получение записи о пользователе с id {id}", id);

            var result = await _inner.GetFullByIdAsync(id);

            return result;
        }
    }
}
