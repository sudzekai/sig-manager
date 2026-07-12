using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Domain.Models.InfoShifts;
using Domain.Models.Parks;
using Domain.ValueObjects.Shifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.InfoShifts
{
    public class InfoShiftRepositoryDecorator(IInfoShiftRepository inner, ILogger<IInfoShiftRepository> logger) : IInfoShiftRepository
    {
        public async Task<ShiftId> AddAsync(InfoShift infoShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("info_shift", "create");

            logger.LogInformation("Создание записи об информационной смене");

            return await inner.AddAsync(infoShift);
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("info_shift", "delete");

            logger.LogInformation("Удаление записи об информационной смене с id {id}", id);

            return await inner.DeleteAsync(id);
        }

        public async Task<InfoShift?> GetAsync(ShiftId id)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("info_shift", "get");

            logger.LogInformation("Восстановление записи об информационной смене с id {id}", id);

            return await inner.GetAsync(id);
        }

        public async Task UpdateAsync(InfoShift infoShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("info_shift", "update");

            logger.LogInformation("Обновление записи об информационной смене с id {id}", infoShift.ShiftId.Value);

            await inner.UpdateAsync(infoShift);
        }
    }
}
