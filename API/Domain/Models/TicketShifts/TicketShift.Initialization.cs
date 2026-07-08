using Domain.Models.Base;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Ticket;

namespace Domain.Models.TicketShifts
{
    public partial class TicketShift : DomainModelBase
    {
        public TicketShift(ShiftFirstTicket firstTicket, ShiftTicketPrice ticketPrice)
        {
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

        public static TicketShift Create(ShiftFirstTicket firstTicket, ShiftTicketPrice ticketPrice)
            => new(firstTicket, ticketPrice);

        public static TicketShift Restore(ShiftId shiftId, ShiftFirstTicket firstTicket, ShiftLastTicket? lastTicket, ShiftTicketPrice ticketPrice)
            => new(shiftId, firstTicket, lastTicket, ticketPrice);
    }
}
