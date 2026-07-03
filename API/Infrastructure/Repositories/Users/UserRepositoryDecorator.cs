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
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Получение списка пользователей из БД");

            return await _inner.GetAllAsync(request);
        }

        public async Task<User?> GetInfoByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Получение информации о пользователе по Id из БД");

            return await _inner.GetInfoByIdAsync(id);
        }

        public async Task<User?> GetInfoByUsernameAsync(string username)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Получение информации о пользователе по Username из БД");

            return await _inner.GetInfoByUsernameAsync(username);
        }

        public async Task<int> CreateAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Создание записи о пользователе в БД");

            return await _inner.CreateAsync(user);
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Удаление записи о пользователе в БД");

            await _inner.DeleteByIdAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Обновление записи о пользователе в БД");

            await _inner.UpdateAsync(user);
        }

        public async Task<User?> GetInfoByEmailAsync(string email)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Получение информации о пользователе по Email из БД");

            return await _inner.GetInfoByEmailAsync(email);
        }

        public async Task<User?> GetInfoByPhoneNumberAsync(string phoneNumber)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Получение информации о пользователе по PhoneNumber из БД");

            return await _inner.GetInfoByEmailAsync(phoneNumber);
        }

        public async Task<User?> GetFullByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRichActivity();
            _logger.LogInformation("Получение записи о пользователе по Id из БД");

            return await _inner.GetFullByIdAsync(id);
        }
    }
}
