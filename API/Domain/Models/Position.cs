using Domain.Tools;

namespace Domain.Models
{
    public class Position
    {
        private Position() { }

        public Position(string name, decimal pricePerHour)
        {
            ChangeName(name);
            ChangePricePerHour(pricePerHour);
        }

        public static Position Restore(int id, string name, decimal pricePerHour) => new() { Id = id, Name = name, PricePerHour = pricePerHour };

        public int Id { get; private set; }

        public string Name { get; private set; }

        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 50, nameof(name));
        }

        public void ChangeName(string value)
        {
            ValidateName(value);

            Name = value;
        }

        public decimal PricePerHour { get; private set; }

        private void ValidatePricePerHour(decimal pricePerHour)
        {
            DataValidator.Min(pricePerHour, 0, nameof(pricePerHour));
        }

        public void ChangePricePerHour(decimal value)
        {
            ValidatePricePerHour(value);

            PricePerHour = value;
        }
    }
}
