using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Roles;
using Domain.ValueObjects.Roles;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Roles
{
    internal class RoleRepositoryDecorator(IRoleRepository inner, ILogger<IRoleRepository> logger) : IRoleRepository
    {
        public async Task<RoleId> AddAsync(Role role)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "create");

            logger.LogInformation("Создание записи о роли");

            return await inner.AddAsync(role);
        }

        public async Task<bool> DeleteAsync(RoleId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "delete");

            logger.LogInformation("Удаление записи о роли с id {id}", id.Value);

            return await inner.DeleteAsync(id);
        }

        public async Task<Role?> GetAsync(RoleId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "get");

            logger.LogInformation("Восстановление записи о роли с id {id}", id.Value);

            return await inner.GetAsync(id);
        }

        public async Task<RoleId?> GetIdByName(RoleName name)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "get");

            logger.LogInformation("Поиски записи о роли с name {name}", name.Value);

            return await inner.GetIdByName(name);
        }

        public async Task UpdateAsync(Role role)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role", "update");

            logger.LogInformation("Обновление записи о роли с id {id}", role.Id.Value);

            await inner.UpdateAsync(role);
        }
    }
}
