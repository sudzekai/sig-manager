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
    }
}
