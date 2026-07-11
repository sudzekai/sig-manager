using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.CarShifts;
using Domain.ValueObjects.Shifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.CarShifts
{
    public class CarShiftRepositoryDecorator(ICarShiftRepository inner, ILogger<ICarShiftRepository> logger) : ICarShiftRepository
    {
        public async Task<ShiftId> AddAsync(CarShift carShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car_shift", "create");

            logger.LogInformation("Создание записи о смене машинок");

            return await inner.AddAsync(carShift);
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car_shift", "delete");

            logger.LogInformation("Удаление записи о смене машинок с id {id}", id);

            return await inner.DeleteAsync(id);
        }

        public async Task<CarShift?> GetAsync(ShiftId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car_shift", "get");

            logger.LogInformation("Восстановление записи о смене машинок с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task UpdateAsync(CarShift carShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("car_shift", "update");

            logger.LogInformation("Обновление записи о смене машинок с id {id}", carShift.ShiftId.Value);

            await inner.UpdateAsync(carShift);
        }
    }
}
