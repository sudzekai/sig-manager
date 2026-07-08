using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Ticket;

namespace Domain.Models.TicketShifts
{
    public partial class TicketShift
    {
        public ShiftId? ShiftId
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ShiftFirstTicket FirstTicket
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ShiftLastTicket? LastTicket
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ShiftTicketPrice TicketPrice
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public int? TotalTickets
        {
            get
            {
                if (LastTicket is not null)
                    return LastTicket.Value - FirstTicket.Value;

                return null;
            }
        }
    }
}
