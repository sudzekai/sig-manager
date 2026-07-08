using Domain.Models.Base;
using Domain.ValueObjects.Cars;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.CarShiftCars
{
    public partial class CarShiftCar : DomainModelBase
    {
        private CarShiftCar(ShiftId shiftId, CarId carId)
        {
            ShiftId = shiftId;
            CarId = carId;
        }

        public static CarShiftCar Create(ShiftId shiftId, CarId carId)
           => new(shiftId, carId);
    }
}
