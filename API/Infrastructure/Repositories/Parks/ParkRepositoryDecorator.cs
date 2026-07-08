using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Parks;
using Domain.ValueObjects.Parks;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Parks
{
    public class ParkRepositoryDecorator(IParkRepository inner, ILogger<IParkRepository> logger) : IParkRepository
    {
        public async Task<ParkId> AddAsync(Park park)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("park", "create");

            logger.LogInformation("Создание записи о парке");

            return await inner.AddAsync(park);
        }

        public async Task<bool> DeleteAsync(ParkId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("park", "delete");

            logger.LogInformation("Удаление записи о парке с id {id}", id);

            return await inner.DeleteAsync(id);
        }

        public async Task<Park?> GetAsync(ParkId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("park", "get");

            logger.LogInformation("Восстановление записи о парке с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task<ParkId?> GetIdByNameAsync(ParkName name)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("park", "get_id_by_name");

            logger.LogInformation("Поиск записи о парке с name {name}", name);

            return await inner.GetIdByNameAsync(name);
        }

        public async Task UpdateAsync(Park park)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("park", "update");

            logger.LogInformation("Обновление записи о парке с id {id}", park.Id);

            await inner.UpdateAsync(park);
        }
    }
}
