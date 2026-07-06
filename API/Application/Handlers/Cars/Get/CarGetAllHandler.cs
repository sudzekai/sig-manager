using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Commands.Cars.Get;
using Contracts.Objects.Dtos.Car;

namespace Application.Handlers.Cars.Get
{
    public class CarGetAllHandler(ICarQuery query) : ICommandHandler<CarGetAllCommand, IReadOnlyList<CarSimpleDto>>
    {
        public async Task<IReadOnlyList<CarSimpleDto>> HandleAsync(CarGetAllCommand command)
            => await query.GetAllAsync(command.Request);
    }
}
