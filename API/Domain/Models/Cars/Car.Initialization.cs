using Domain.Models.Base;
using Domain.ValueObjects.Cars;

namespace Domain.Models.Cars
{
    public partial class Car : DomainModelBase
    {
        private Car(CarId id, CarName name, CarPlate plate, CarStatus status)
        {
            Id = id;
            Name = name;
            Plate = plate;
            Status = status;

            _initialized = true;
        }

        private Car(CarId id, CarName name, CarPlate plate)
        {
            Id = id;
            Name = name;
            Plate = plate;
            Status = CarStatus.Working;

            _initialized = true;
        }

        internal static Car Restore(CarId id, CarName name, CarPlate plate, CarStatus status)
            => new(id, name, plate, status);

        public static Car Create(CarId id, CarName name, CarPlate plate)
            => new(id, name, plate);
    }
}
