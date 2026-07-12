using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Commands.CarShifts;
using Contracts.Objects.Dtos.CarShift;
using Domain.Models.InfoShifts;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Info;
using Domain.ValueObjects.Shifts.Ticket;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.CarShifts
{
    public class CarShiftCloseHandler(
        IInfoShiftRepository infos,
        IShiftRepository shifts,
        ITicketShiftRepository tickets,
        IUnitOfWork uow,
        ICarShiftQuery query
        ) : ICommandHandler<CarShiftCloseCommand, CarShiftInfoDto>
    {
        public async Task<CarShiftInfoDto> HandleAsync(CarShiftCloseCommand command)
        {
            await uow.BeginTransactionAsync();

            var shiftId = ShiftId.FromValue(command.Id);

            var shift = await shifts.GetAsync(shiftId);

            if (shift is null || shift.Type != ShiftType.Car || shift.Status == ShiftStatus.Closed)
                throw NotFoundException.ShiftWithId(command.Id);

            var ticketShift = await tickets.GetAsync(shiftId)
                ?? throw NotFoundException.ShiftWithId(command.Id);

            shift.Close();

            ticketShift.LastTicket = ShiftLastTicket.FromValue(command.Dto.LastTicket, ticketShift.FirstTicket);

            var info = InfoShift.Create(
                shiftId,
                ShiftCash.FromValue(command.Dto.Cash),
                ShiftCashless.FromValue(command.Dto.Cashless),
                ShiftReceiptPhotoFileName.FromValue("test")
            );

            await shifts.UpdateAsync(shift);
            await tickets.UpdateAsync(ticketShift);
            await infos.AddAsync(info);

            await uow.CommitAsync();

            return await query.GetByIdAsync(shiftId.Value)
                ?? throw NotFoundException.ShiftWithId(shiftId.Value);
        }
    }
}
