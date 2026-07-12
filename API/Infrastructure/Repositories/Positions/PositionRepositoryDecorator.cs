using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Positions;
using Domain.ValueObjects.Positions;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Positions
{
    public class PositionRepositoryDecorator(IPositionRepository inner, ILogger<IPositionRepository> logger) : IPositionRepository
    {
        public async Task<PositionId> AddAsync(Position position)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("position", "create");

            logger.LogInformation("Создание записи о должности");

            return await inner.AddAsync(position);
        }

        public async Task<bool> DeleteAsync(PositionId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("position", "delete");

            logger.LogInformation("Удаление записи о должности с id {id}", id.Value);

            return await inner.DeleteAsync(id);
        }

        public async Task<Position?> GetAsync(PositionId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("position", "get");

            logger.LogInformation("Восстановление записи о должности с id {id}", id.Value);

            return await inner.GetAsync(id);
        }

        public async Task<PositionId?> GetIdByNameAsync(PositionName name)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("position", "get_id_by_name");

            logger.LogInformation("Поиск записи о должности с name {name}", name.Value);

            return await inner.GetIdByNameAsync(name);
        }

        public async Task UpdateAsync(Position position)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("position", "update");

            logger.LogInformation("Обновление записи о должности с id {id}", position.Id.Value);

            await inner.UpdateAsync(position);
        }
    }
}
