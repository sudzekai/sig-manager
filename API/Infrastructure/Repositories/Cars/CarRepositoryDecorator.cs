using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepositoryDecorator(ICarRepository inner, ILogger<ICarRepository> logger) : ICarRepository
    {
        public async Task<int> AddAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "create");

            logger.LogInformation("Создание записи о машине");

            return await inner.AddAsync(car);
        }

        public async Task DeleteAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "delete");

            logger.LogInformation("Удаление записи о машине с id {id}", id);

            await inner.DeleteAsync(id);
        }

        public async Task<Car?> GetAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get");

            logger.LogInformation("Восстановление записи о машине с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task<int?> GetIdByNameAsync(string name)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_id_by_name");
            
            logger.LogInformation("Поиск записи о машине с name {name}", name);

            return await inner.GetIdByNameAsync(name);
        }

        public async Task UpdateAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "update");

            logger.LogInformation("Обновление записи о машине с id {id}", car.Id);

            await inner.UpdateAsync(car);
        }
    }
}