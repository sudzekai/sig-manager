using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Users;
using Domain.ValueObjects.Users;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Users
{
    public class UserRepositoryDecorator(IUserRepository inner, ILogger<IUserRepository> logger) : IUserRepository
    {
        public async Task<UserId> AddAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "create");

            logger.LogInformation("Создание записи о пользователе");

            return await inner.AddAsync(user);
        }

        public async Task<bool> DeleteAsync(UserId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "delete");

            logger.LogInformation("Удаление записи о пользователе с id {id}", id.Value);

            return await inner.DeleteAsync(id);
        }

        public async Task<UserId?> GetIdByUsernameAsync(Username username)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_id_by_username");

            logger.LogInformation("Поиск записи о пользователе с username {username}", username.Value);

            return await inner.GetIdByUsernameAsync(username);
        }

        public async Task<UserId?> GetIdByEmailAsync(UserEmail email)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_id_by_email");

            logger.LogInformation("Поиск записи о пользователе с email {email}", email.Value);

            return await inner.GetIdByEmailAsync(email);
        }

        public async Task<UserId?> GetIdByPhoneNumberAsync(UserPhoneNumber phoneNumber)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get_id_by_phoneNumber");

            logger.LogInformation("Поиск записи о пользователе с phoneNumber {phoneNumber}", phoneNumber.Value);

            return await inner.GetIdByPhoneNumberAsync(phoneNumber);
        }

        public async Task<User?> GetAsync(UserId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "get");

            logger.LogInformation("Восстановление записи о пользователе с id {id}", id.Value);

            return await inner.GetAsync(id);
        }
        public async Task UpdateAsync(User user)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user", "update");

            logger.LogInformation("Обновление записи о пользователе c id {id}", user.Id);

            await inner.UpdateAsync(user);
        }
    }
}
