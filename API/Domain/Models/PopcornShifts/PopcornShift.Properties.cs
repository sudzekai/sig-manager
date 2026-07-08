using Domain.ValueObjects.Shifts;

namespace Domain.Models.PopcornShifts
{
    public partial class PopcornShift
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
