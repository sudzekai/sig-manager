using Domain.ValueObjects.Shifts;

namespace Domain.Models.AdminShifts
{
    public partial class AdminShift
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
