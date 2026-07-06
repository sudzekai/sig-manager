using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Car;

namespace Contracts.Objects.Commands.Cars.Write
{
    public record CarCreateCommand(CarCreateDto Dto) : ICommand<CarInfoDto>;
}
