using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Roles;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Queries.Roles
{
    public class RoleQueryDecorator(IRoleQuery inner, ILogger<IRoleQuery> logger) : IRoleQuery
    {
        public async Task<IReadOnlyList<RoleSimpleDto>> GetAllAsync()
        {
            using var activity = Telemetry.Repository.StartQueryActivity("role", "get_all");

            logger.LogInformation("Получение списка записей о ролях");

            return await inner.GetAllAsync();
        }

        public async Task<RoleInfoDto?> GetByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("role", "get_by_id");

            logger.LogInformation("Получение информации о роли с id {id}", id);

            return await inner.GetByIdAsync(id);
        }
    }
}
