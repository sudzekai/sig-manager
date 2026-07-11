using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Shifts;
using Domain.ValueObjects.Shifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Shifts
{
    public class ShiftRepositoryDecorator(IShiftRepository inner, ILogger<IShiftRepository> logger) : IShiftRepository
    {
        public async Task<ShiftId> AddAsync(Shift shift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "create");

            logger.LogInformation("Создание записи о смене");

            return await inner.AddAsync(shift);
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "delete");

            logger.LogInformation("Удаление записи о смене с id {id}", id.Value);

            return await inner.DeleteAsync(id);
        }

        public async Task<Shift?> GetAsync(ShiftId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "get");

            logger.LogInformation("Восстановление записи о смене с id {id}", id.Value);

            return await inner.GetAsync(id);
        }

        public async Task UpdateAsync(Shift shift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "update");

            logger.LogInformation("Обновление записи о смене с id {id}", shift.Id.Value);

            await inner.UpdateAsync(shift);
        }
    }
}
