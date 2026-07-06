using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Queries.Cars;
using Shared.Types.Exceptions;

namespace Application.QueryHandlers.Cars
{
    public class CarGetHandler(ICarQuery query) : IQueryHandler<CarGetQuery, CarInfoDto>
    {
        public async Task<CarInfoDto> HandleAsync(CarGetQuery command)
            => await query.GetByIdAsync(command.Id)
                ?? throw NotFoundException.CarWithId(command.Id);
    }
}
