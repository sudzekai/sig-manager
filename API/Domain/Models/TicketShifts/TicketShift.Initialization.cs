using Domain.Models.Base;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Ticket;

namespace Domain.Models.TicketShifts
{
    public partial class TicketShift : DomainModelBase
    {
        public TicketShift(ShiftId shiftId, ShiftFirstTicket firstTicket, ShiftTicketPrice ticketPrice)
        {
            ShiftId = shiftId;
            FirstTicket = firstTicket;
            TicketPrice = ticketPrice;
        }

        public TicketShift(ShiftId shiftId, ShiftFirstTicket firstTicket, ShiftLastTicket? lastTicket, ShiftTicketPrice ticketPrice)
        {
            ShiftId = shiftId;
            FirstTicket = firstTicket;
            LastTicket = lastTicket;
            TicketPrice = ticketPrice;
        }

        public static TicketShift Create(ShiftId shiftId, ShiftFirstTicket firstTicket, ShiftTicketPrice ticketPrice)
            => new(shiftId, firstTicket, ticketPrice);

        public static TicketShift Restore(ShiftId shiftId, ShiftFirstTicket firstTicket, ShiftLastTicket? lastTicket, ShiftTicketPrice ticketPrice)
            => new(shiftId, firstTicket, lastTicket, ticketPrice);
    }
}
