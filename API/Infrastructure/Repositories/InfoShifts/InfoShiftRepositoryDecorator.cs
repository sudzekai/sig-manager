using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Domain.Models.InfoShifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.InfoShifts
{
    public class InfoShiftRepositoryDecorator(IInfoShiftRepository inner, ILogger<IInfoShiftRepository> logger) : IInfoShiftRepository
    {
        public async Task<int> AddAsync(InfoShift infoShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("infoShift", "create");

            logger.LogInformation("Создание записи об информационной смене");

            return await inner.AddAsync(infoShift);
        }

        public async Task DeleteAsync(int shiftId)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("infoShift", "delete");

            logger.LogInformation("Удаление записи об информационной смене с shift_id {shift_id}", shiftId);

            await inner.DeleteAsync(shiftId);
        }

        public async Task<InfoShift?> GetAsync(int shiftId)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("infoShift", "get");

            logger.LogInformation("Восстановление записи об информационной смене с shift_id {shift_id}", shiftId);

            return await inner.GetAsync(shiftId);
        }

        public async Task UpdateAsync(InfoShift infoShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("infoShift", "update");

            logger.LogInformation("Обновление записи об информационной смене с shift_id {shift_id}", infoShift.ShiftId);

            await inner.UpdateAsync(infoShift);
        }
    }
}
