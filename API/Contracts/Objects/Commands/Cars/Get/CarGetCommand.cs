using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Car;

namespace Contracts.Objects.Commands.Cars.Get
{
    public record CarGetCommand(int Id) : ICommand<CarInfoDto>;
}
