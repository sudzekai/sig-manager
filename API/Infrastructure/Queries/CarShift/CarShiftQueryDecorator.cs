using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Queries.CarShift
{
    public class CarShiftQueryDecorator(ICarShiftQuery inner, ILogger<ICarShiftQuery> logger) : ICarShiftQuery
    {
        public async Task<IReadOnlyList<CarShiftSimpleDto>> GetAllAsync(GetCarShiftsListRequest request)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("car_shift", "get_all");

            logger.LogInformation("Получение списка записей о сменах машинок");

            return await inner.GetAllAsync(request);
        }

        public async Task<CarShiftInfoDto?> GetByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("car_shift", "get_by_id");

            logger.LogInformation("Получение информации о смене машин с id {id}", id);

            return await inner.GetByIdAsync(id);
        }
    }
}
