using Domain.Models.InfoShifts;
using Domain.Models.Shifts;
using Domain.Models.TicketShifts;

namespace Domain.Models.CarShifts
{
    public partial class CarShift
    {
        private CarShift(int firstTicket, decimal ticketPrice)
        {
            _shift = Shift.Create("cars");
            _ticketShift = TicketShift.Create(firstTicket, ticketPrice);

            _initialized = true;
        }

        private CarShift(int shiftId, Shift shift, InfoShift? infoShift, TicketShift ticketShift)
        {
            _shiftId = shiftId;
            _shift = shift;
            _ticketShift = ticketShift;
            _infoShift = infoShift;

            _initialized = true;
        }

        public static CarShift Restore(int shiftId, Shift shift, InfoShift? infoShift, TicketShift ticketShift)
            => new(shiftId, shift, infoShift, ticketShift);

        public static CarShift Create(int firstTicket, decimal ticketPrice)
            => new(firstTicket, ticketPrice);

        private readonly bool _initialized = false;
    }
}
