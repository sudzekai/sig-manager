using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Domain.Models.Shifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.Shifts
{
    internal class ShiftRepositoryDecorator(IShiftRepository inner, ILogger<IShiftRepository> logger) : IShiftRepository
    {
        public async Task<int> AddAsync(Shift shift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "create");

            logger.LogInformation("Создание записи о смене");

            return await inner.AddAsync(shift);
        }

        public async Task DeleteAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "delete");

            logger.LogInformation("Удаление записи о смене с id {id}", id);

            await inner.DeleteAsync(id);
        }

        public async Task<Shift?> GetAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "get");

            logger.LogInformation("Восстановление записи о смене с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task UpdateAsync(Shift shift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("shift", "update");

            logger.LogInformation("Обновление записи о смене с id {id}", shift.Id);

            await inner.UpdateAsync(shift);
        }
    }
}
