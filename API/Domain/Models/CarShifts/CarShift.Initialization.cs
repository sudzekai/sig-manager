using Domain.Models.InfoShifts;
using Domain.Models.Shifts;
using Domain.Models.TicketShifts;

namespace Domain.Models.CarShifts
{
    public partial class CarShift
    {
        private CarShift(int shiftId, Shift shift, TicketShift ticketShift)
        {
            _shiftId = shiftId;
            _shift = shift;
            _ticketShift = ticketShift;

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

        public static CarShift Create(int shiftId, Shift shift, TicketShift ticketShift)
            => new(shiftId, shift, ticketShift);

        private readonly bool _initialized = false;
    }
}
