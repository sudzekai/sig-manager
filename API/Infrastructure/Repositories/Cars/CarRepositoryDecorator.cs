using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Cars;
using Domain.ValueObjects.Cars;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepositoryDecorator(ICarRepository inner, ILogger<ICarRepository> logger) : ICarRepository
    {
        public async Task<CarId> AddAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "create");

            logger.LogInformation("Создание записи о машине");

            return await inner.AddAsync(car);
        }

        public async Task<bool> DeleteAsync(CarId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "delete");

            logger.LogInformation("Удаление записи о машине с id {id}", id.Value);

            return await inner.DeleteAsync(id);
        }

        public async Task<Car?> GetAsync(CarId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get");

            logger.LogInformation("Восстановление записи о машине с id {id}", id.Value);

            return await inner.GetAsync(id);
        }

        public async Task<CarId?> GetIdByNameAsync(CarName name)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_id_by_name");

            logger.LogInformation("Поиск записи о машине с name {name}", name.Value);

            return await inner.GetIdByNameAsync(name);
        }

        public async Task UpdateAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "update");

            logger.LogInformation("Обновление записи о машине с id {id}", car.Id.Value);

            await inner.UpdateAsync(car);
        }
    }
}