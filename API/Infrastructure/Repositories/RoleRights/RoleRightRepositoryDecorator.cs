using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.RoleRight;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.RoleRights
{
    public class RoleRightRepositoryDecorator(IRoleRightRepository inner, ILogger<IRoleRightRepository> logger) : IRoleRightRepository
    {
        public async Task<bool> AddAsync(RoleRight roleRight)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role_right", "create");

            logger.LogInformation("Создание связи между ролью и правом");

            return await inner.AddAsync(roleRight);
        }

        public async Task<bool> DeleteAsync(RoleRight roleRight)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("role_right", "delete");

            logger.LogInformation("Удаление связи между ролью и правом");

            return await inner.DeleteAsync(roleRight);
        }
    }
}
