using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.CarShift;

namespace Contracts.Objects.Commands.CarShifts
{
    public record CarShiftOpenCommand(CarShiftOpenDto Dto) : ICommand<CarShiftInfoDto>;
}
