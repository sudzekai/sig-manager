using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class Car
    {
        // ctors

        private Car(int id, string name, int number, string plate, string status, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Number = number;
            Plate = plate;
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        private Car(string name, int number, string plate)
        {
            SetName(name);
            SetNumber(number);
            SetPlate(plate);
            SetStatus("working");

            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }

        // statics

        internal static Car Restore(int id, string name, int number, string plate, string status, DateTime createdAt, DateTime updatedAt)
            => new(id, name, number, plate, status, createdAt, updatedAt);

        public static Car Create(string name, int number, string plate)
            => new(name, number, plate);

        // props

        public int Id { get; private set; } = default;
        public string Name { get; private set; }
        public int Number { get; private set; }
        public string Plate { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // private setters

        [MemberNotNull(nameof(Name))]
        private void SetName(string value)
        {
            ValidateName(value);

            Name = value;
        }

        [MemberNotNull(nameof(Number))]
        private void SetNumber(int value)
        {
            ValidateNumber(value);

            Number = value;
        }

        [MemberNotNull(nameof(Plate))]
        private void SetPlate(string value)
        {
            ValidatePlate(value);

            Plate = value;
        }

        [MemberNotNull(nameof(Status))]
        private void SetStatus(string value)
        {
            ValidateStatus(value);

            Status = value;
        }

        [MemberNotNull(nameof(UpdatedAt))]
        private void Touch() => UpdatedAt = DateTime.Now;


        // public setters

        public void ChangeName(string value)
        {
            if (Name == value)
                return;

            SetName(value);

            Touch();
        }

        public void ChangeNumber(int value)
        {
            if (Number == value)
                return;

            SetNumber(value);

            Touch();
        }

        public void ChangePlate(string value)
        {
            if (Plate == value)
                return;

            SetPlate(value);

            Touch();
        }

        public void ChangeStatus(string value)
        {
            if (Status == value)
                return;

            SetStatus(value);

            Touch();
        }

        // validators

        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 50, nameof(name));
        }

        private void ValidateNumber(int number)
        {
            DataValidator.Min(number, 1, nameof(number));
        }

        private void ValidatePlate(string plate)
        {
            DataValidator.NullOrWhiteSpace(plate, nameof(plate));
            DataValidator.MaxLength(plate, 100, nameof(plate));
        }

        private void ValidateStatus(string status)
        {
            DataValidator.NullOrWhiteSpace(status, nameof(status));
            DataValidator.OneOf(status, ["working", "broken"], nameof(status));
        }
    }
}
