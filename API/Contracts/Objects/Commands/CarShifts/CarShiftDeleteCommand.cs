using Contracts.Interfaces.Application.Commands;

namespace Contracts.Objects.Commands.CarShifts
{
    public record CarShiftDeleteCommand(int Id) : ICommand<Unit>;
}
