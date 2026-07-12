using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.UserShifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.UserShifts
{
    public class UserShiftRepositoryDecorator(IUserShiftRepository inner, ILogger<IUserShiftRepository> logger) : IUserShiftRepository
    {
        public async Task AddAsync(UserShift userShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user_shift", "create");

            logger.LogInformation("Создание связи между сменой с id {id} и пользователем с id {id}", userShift.ShiftId.Value, userShift.UserId.Value);

            await inner.AddAsync(userShift);
        }

        public async Task DeleteAsync(UserShift userShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("user_shift", "delete");

            logger.LogInformation("Удаление связи между сменой с id {id} и пользователем с id {id}", userShift.ShiftId.Value, userShift.UserId.Value);

            await inner.DeleteAsync(userShift);
        }
    }
}
