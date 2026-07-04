using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Roles
{
    internal class RoleRepositoryDecorator(IRoleRepository inner, ILogger<IRoleRepository> logger) : IRoleRepository
    {
        public async Task<int> AddAsync(Role role)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "create");

            logger.LogInformation("Создание записи о роли");

            return await inner.AddAsync(role);
        }

        public async Task DeleteAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "delete");

            logger.LogInformation("Удаление записи о роли с id {id}", id);

            await inner.DeleteAsync(id);
        }

        public async Task<Role?> GetAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "get");

            logger.LogInformation("Восстановление записи о роли с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task UpdateAsync(Role role)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "update");

            logger.LogInformation("Обновление записи о роли с id {id}", role.Id);

            await inner.UpdateAsync(role);
        }
    }
}
