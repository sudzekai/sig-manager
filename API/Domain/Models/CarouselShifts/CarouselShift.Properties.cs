using Domain.ValueObjects.Shifts;

namespace Domain.Models.CarouselShifts
{
    public partial class CarouselShift
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
