using Domain.ValueObjects.Shifts;

namespace Domain.Models.TrainShifts
{
    public partial class TrainShift
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
