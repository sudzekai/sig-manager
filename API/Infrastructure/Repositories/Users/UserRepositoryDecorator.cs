using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Users
{
    public class UserRepositoryDecorator(IUserRepository inner, ILogger<IUserRepository> logger) : IUserRepository
    {
        public async Task<int> AddAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "create");

            logger.LogInformation("Создание записи о пользователе");

            return await inner.AddAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "delete");

            logger.LogInformation("Удаление записи о пользователе с id {id}", id);

            await inner.DeleteAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "update");

            logger.LogInformation("Обновление записи о пользователе c id {id}", user.Id);

            await inner.UpdateAsync(user);
        }

        public async Task<User?> GetAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get");

            logger.LogInformation("Восстановление записи о пользователе с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task<int?> GetIdByUsernameAsync(string username)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_id_by_username");

            logger.LogInformation("Поиск записи о пользователе с username {username}", username);

            return await inner.GetIdByUsernameAsync(username);
        }

        public async Task<int?> GetIdByEmailAsync(string email)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_id_by_email");

            logger.LogInformation("Поиск записи о пользователе с email {email}", email);

            return await inner.GetIdByEmailAsync(email);
        }

        public async Task<int?> GetIdByPhoneNumberAsync(string phoneNumber)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_id_by_phoneNumber");

            logger.LogInformation("Поиск записи о пользователе с phoneNumber {phoneNumber}", phoneNumber);

            return await inner.GetIdByPhoneNumberAsync(phoneNumber);
        }
    }
}
