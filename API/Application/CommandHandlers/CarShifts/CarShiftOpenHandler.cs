using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Commands.CarShifts;
using Contracts.Objects.Dtos.CarShift;
using Domain.Models.CarShifts;
using Domain.Models.Shifts;
using Domain.Models.TicketShifts;
using Domain.Models.UserShifts;
using Domain.ValueObjects.Parks;
using Domain.ValueObjects.Positions;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Ticket;
using Domain.ValueObjects.Users;
using Shared.App;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.CarShifts
{
    public class CarShiftOpenHandler(
        ICarShiftRepository carShifts,
        IShiftRepository shifts,
        ITicketShiftRepository tickets,
        IUserShiftRepository userShifts,
        IUserRepository users,
        IPositionRepository positions,
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

            AppConstants.StandartWriter.WriteLine();
            AppConstants.StandartWriter.WriteLine(command.Dto.Users.Length);
            AppConstants.StandartWriter.WriteLine();

            foreach (var user in command.Dto.Users)
            {
                var userId = UserId.FromValue(user.Id);
                var positionId = PositionId.FromValue(user.PositionId);

                _ = await users.GetAsync(userId)
                    ?? throw NotFoundException.UserWithId(user.Id);
                _ = await positions.GetAsync(positionId)
                    ?? throw NotFoundException.PositionWithId(user.PositionId);

                await userShifts.AddAsync(
                    UserShift.Create(userId, shiftId, positionId)
                );
            }

            await uow.CommitAsync();

            return new(
                shiftId.Value,
                carShift.ParkId.Value,
                shift.Status.Value,
                shift.CreatedAt,
                shift.ClosedAt,
                null,
                null,
                null,
                null,
                null,
                command.Dto.FirstTicket,
                null,
                null,
                command.Dto.TicketPrice,
                command.Dto.Users
                );
        }
    }
}
