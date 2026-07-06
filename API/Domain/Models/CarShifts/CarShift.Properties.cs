using Domain.Models.InfoShifts;
using Domain.Models.Shifts;
using Domain.Models.TicketShifts;
using Domain.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Models.CarShifts
{
    public partial class CarShift : INotifyPropertyChanged
    {
        private int _shiftId;
        private Shift _shift;
        private InfoShift? _infoShift;
        private TicketShift _ticketShift;

        public int ShiftId
        {
            get => _shiftId;
            set
            {
                DataValidator.Null(value, nameof(ShiftId));

                _shiftId = value;

                OnPropertyChanged();
            }
        }

        public Shift Shift
        {
            get => _shift;
            set
            {
                DataValidator.Null(value, nameof(Shift));
                
                _shift = value;

                OnPropertyChanged();
            }
        }

        public InfoShift? InfoShift
        {
            get => _infoShift;
            set
            {
                DataValidator.Null(value, nameof(InfoShift));
                
                _infoShift = value;
            
                OnPropertyChanged();
            }
        }

        public TicketShift TicketShift
        {
            get => _ticketShift;
            set
            {
                DataValidator.Null(value, nameof(TicketShift));

                _ticketShift = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
