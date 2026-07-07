using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Models.TicketShifts
{
    public partial class TicketShift : INotifyPropertyChanged
    {
        private int _shiftId;

        private int _firstTicket;

        private int? _lastTicket;

        private decimal _ticketPrice;


        public int ShiftId
        {
            get => _shiftId;
            set
            {
                if (_shiftId == value)
                    return;

                ValidateShiftId(value);

                _shiftId = value;

                OnPropertyChanged();
            }
        }

        public int FirstTicket
        {
            get => _firstTicket;
            set
            {
                if (_firstTicket == value)
                    return;

                ValidateFirstTicket(value);

                _firstTicket = value;

                OnPropertyChanged();
            }
        }

        public int? LastTicket
        {
            get => _lastTicket;
            set
            {
                if (_lastTicket == value)
                    return;

                ValidateLastTicket(value);

                _lastTicket = value;

                OnPropertyChanged();
            }
        }

        public decimal TicketPrice
        {
            get => _ticketPrice;
            set
            {
                if (_ticketPrice == value)
                    return;

                ValidateTicketPrice(value);

                _ticketPrice = value;

                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
