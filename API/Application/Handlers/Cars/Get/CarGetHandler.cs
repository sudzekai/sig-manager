using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Commands.Cars.Get;
using Contracts.Objects.Dtos.Car;
using Shared.Types.Exceptions;

namespace Application.Handlers.Cars.Get
{
    public class CarGetHandler(ICarQuery query) : ICommandHandler<CarGetCommand, CarInfoDto>
    {
        public async Task<CarInfoDto> HandleAsync(CarGetCommand command)
            => await query.GetByIdAsync(command.Id) 
                ?? throw NotFoundException.CarWithId(command.Id);
    }
}
