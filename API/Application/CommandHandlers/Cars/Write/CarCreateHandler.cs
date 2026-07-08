using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Commands.Cars.Write;
using Contracts.Objects.Dtos.Car;
using Domain.Models.Cars;
using Domain.ValueObjects.Cars;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Cars.Write
{
    public class CarCreateHandler(ICarRepository repository, ICarQuery query) : ICommandHandler<CarCreateCommand, CarInfoDto>
    {
        public async Task<CarInfoDto> HandleAsync(CarCreateCommand command)
        {
            var name = CarName.FromValue(command.Dto.Name);

            if (repository.GetIdByNameAsync(name) is not null)
                throw ConflictException.CarName;

            var id = CarId.FromValue(command.Dto.Id);

            if (await repository.GetAsync(id) is not null)
                throw ConflictException.CarId;

            Car car = Car.Create(
                id,
                name,
                CarPlate.FromValue(command.Dto.Plate)
            );

            await repository.AddAsync(car);

            return await query.GetByIdAsync(id.Value) 
                ?? throw NotFoundException.CarWithId(id.Value);
        }
    }
}
