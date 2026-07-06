namespace Domain.Models.Cars
{
    public partial class Car
    {
        private Car(int id, string name, string plate, string status)
        {
            _id = id;
            _name = name;
            _plate = plate;
            _status = status;

            _initialized = true;
        }

        private Car(int id, string name, string plate)
        {
            Id = id;
            Name = name;
            Plate = plate;
            Status = "working";

            _initialized = true;
        }

        internal static Car Restore(int id, string name, string plate, string status)
            => new(id, name, plate, status);

        public static Car Create(int id, string name, string plate)
            => new(id, name, plate);

        private bool _initialized = false;
    }
}
