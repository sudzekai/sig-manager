using Domain.ValueObjects.Cars;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.CarShiftCars
{
    public partial class CarShiftCar
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

        public CarId CarId
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
