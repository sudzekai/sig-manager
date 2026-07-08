using Domain.ValueObjects.Shifts;

namespace Domain.Models.BouncerShifts
{
    public partial class BouncerShift
    {
        public ShiftId ShiftId
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
    }
}
