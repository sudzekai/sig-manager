using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Car;

namespace Contracts.Objects.Commands.Cars.Update
{
    public record CarStatusUpdateCommand(int Id, CarStatusUpdateDto Dto) : ICommand<Unit>;
}
