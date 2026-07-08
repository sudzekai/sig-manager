using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Commands.CarShifts;
using Contracts.Objects.Dtos.CarShift;
using Domain.Models.CarShifts;
using Domain.Models.Shifts;
using Domain.Models.TicketShifts;
using Domain.ValueObjects.Parks;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Ticket;
using Shared.App;

namespace Application.CommandHandlers.CarShifts
{
    public class CarShiftOpenHandler(
        ICarShiftRepository carShifts,
        IShiftRepository shifts,
        ITicketShiftRepository tickets,
        IUnitOfWork uow
        ) : ICommandHandler<CarShiftOpenCommand, CarShiftInfoDto>
    {
        public async Task<CarShiftInfoDto> HandleAsync(CarShiftOpenCommand command)
        {
            await uow.BeginTransactionAsync();

            var shift = Shift.Create(ShiftType.Car);
            var shiftId = await shifts.AddAsync(shift);

            var ticketShift = TicketShift.Create(
                shiftId,
                ShiftFirstTicket.FromValue(command.Dto.FirstTicket),
                ShiftTicketPrice.FromValue(command.Dto.TicketPrice)
            );
            await tickets.AddAsync(ticketShift);

            var carShift = CarShift.Create(
                shiftId,
                ParkId.FromValue(command.Dto.ParkId)
            );
            await carShifts.AddAsync(carShift);

            await uow.CommitAsync();

            throw AppConstants.TODO();
        }
    }
}
