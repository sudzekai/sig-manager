using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Queries.Cars;

namespace Application.QueryHandlers.Cars
{
    public class CarGetAllHandler(ICarQuery query) : IQueryHandler<CarGetAllQuery, IReadOnlyList<CarSimpleDto>>
    {
        public async Task<IReadOnlyList<CarSimpleDto>> HandleAsync(CarGetAllQuery command)
            => await query.GetAllAsync(command.Request);

    }
}
