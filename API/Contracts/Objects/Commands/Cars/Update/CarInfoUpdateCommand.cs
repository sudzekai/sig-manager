using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Car;

namespace Contracts.Objects.Commands.Cars.Update
{
    public record CarInfoUpdateCommand(int Id, CarInfoUpdateDto Dto) : ICommand<Unit>;
}
