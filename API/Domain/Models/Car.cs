using Domain.Tools;

namespace Domain.Models
{
    public class Car
    {
        private Car() { }

        public Car(string name, int number, string plate)
        {
            ChangeName(name);
            ChangeNumber(number);
            ChangePlate(plate);
            ChangeStatus("working");

            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }

        internal static Car Restore(int id, string name, int number, string plate, string status, DateTime createdAt, DateTime updatedAt)
            => new()
               {
                   Id = id,
                   Name = name,
                   Number = number,
                   Plate = plate,
                   Status = status,
                   CreatedAt = createdAt,
                   UpdatedAt = updatedAt
               };


        public int Id { get; private set; }
        public string Name { get; private set; }

        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 50, nameof(name));
        }

        public void ChangeName(string value)
        {
            if (Name == value)
                return;

            ValidateName(value);

            Name = value;
            Touch();
        }

        public int Number { get; private set; }

        private void ValidateNumber(int number)
        {
            DataValidator.Min(number, 1, nameof(number));
        }

        public void ChangeNumber(int value)
        {
            if (Number == value)
                return;

            ValidateNumber(value);

            Number = value;
            Touch();
        }

        public string Plate { get; private set; }

        private void ValidatePlate(string plate)
        {
            DataValidator.NullOrWhiteSpace(plate, nameof(plate));
            DataValidator.MaxLength(plate, 100, nameof(plate));
        }

        public void ChangePlate(string value)
        {
            if (Plate == value)
                return;

            ValidatePlate(value);

            Plate = value;
            Touch();
        }

        public string Status { get; private set; }

        private void ValidateStatus(string status)
        {
            DataValidator.NullOrWhiteSpace(status, nameof(status));
            DataValidator.OneOf(status, ["working", "broken"], nameof(status));
        }

        public void ChangeStatus(string value)
        {
            if (Status == value)
                return;

            ValidateStatus(value);

            Status = value;
            Touch();
        }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }
        private void Touch() => UpdatedAt = DateTime.Now;
    }
}
