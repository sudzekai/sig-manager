using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepositoryDecorator(ICarRepository inner, ILogger<CarRepository> logger) : ICarRepository
    {
        private readonly ICarRepository _inner = inner;
        private readonly ILogger<CarRepository> _logger = logger;

        public async Task<int> CreateAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "create");
            _logger.LogInformation("Создание записи о машине");

            return await _inner.CreateAsync(car);
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "delete_by_id");
            _logger.LogInformation("Удаление записи о машине с id {id}", id);

            await _inner.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<Car>> GetAllAsync(GetCarsListRequest request)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_all");
            _logger.LogInformation("Получение списка машин из БД");

            return await _inner.GetAllAsync(request);
        }

        public async Task<Car?> GetFullByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_full_by_id");
            _logger.LogInformation("Получение полной информации о машине с id {id}", id);

            return await _inner.GetFullByIdAsync(id);
        }

        public async Task<Car?> GetInfoByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_info_by_id");
            _logger.LogInformation("Получение информации о машине с id {id}", id);

            return await _inner.GetInfoByIdAsync(id);
        }

        public async Task<Car?> GetInfoByNameAsync(string name)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_info_by_name");
            _logger.LogInformation("Получение информации о машине с name {name}", name);

            return await _inner.GetInfoByNameAsync(name);
        }

        public async Task<Car?> GetInfoByNumberAsync(int number)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "get_info_by_number");
            _logger.LogInformation("Получение информации о машине с number {number}", number);

            return await _inner.GetInfoByNumberAsync(number);
        }

        public async Task UpdateAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car", "update");
            _logger.LogInformation("Обновление записи о машине с id {id}", car.Id);

            await _inner.UpdateAsync(car);
        }
    }
}