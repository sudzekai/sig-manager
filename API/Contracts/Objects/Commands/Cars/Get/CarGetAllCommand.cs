using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Objects.Commands.Cars.Get
{
    public record CarGetAllCommand(GetCarsListRequest Request) : ICommand<IReadOnlyList<CarSimpleDto>>;
}
