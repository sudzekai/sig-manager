namespace Domain.Models.TicketShifts
{
    public partial class TicketShift
    {
        public TicketShift(int firstTicket, decimal ticketPrice)
        {
            FirstTicket = firstTicket;
            TicketPrice = ticketPrice;
        }

        public TicketShift(int shiftId, int firstTicket, int? lastTicket, decimal ticketPrice)
        {
            _shiftId = shiftId;
            _firstTicket = firstTicket;
            _lastTicket = lastTicket;
            _ticketPrice = ticketPrice;
        }

        public static TicketShift Create(int firstTicket, decimal ticketPrice)
            => new(firstTicket, ticketPrice);

        public static TicketShift Restore(int shiftId, int firstTicket, int? lastTicket, decimal ticketPrice)
            => new(shiftId, firstTicket, lastTicket, ticketPrice);

        private bool _initialized = false;
    }
}
