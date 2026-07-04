using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Queries.Cars
{
    internal class CarQueryDecorator(ICarQuery inner, ILogger<ICarQuery> logger) : ICarQuery
    {
        public async Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("car", "get_all");

            logger.LogInformation("Получение списка записей о машинах");

            return await inner.GetAllAsync(request);
        }

        public async Task<CarInfoDto?> GetByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("car", "get_by_id");

            logger.LogInformation("Получение информации о машине с id {id}", id);

            return await inner.GetByIdAsync(id);
        }
    }
}
