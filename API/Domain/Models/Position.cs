using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class Position
    {
        // ctors
        
        private Position(int id, string name, decimal pricePerHour) 
        {
            Id = id;
            Name = name;
            PricePerHour = pricePerHour;
        }

        private Position(string name, decimal pricePerHour)
        {
            SetName(name);
            SetPricePerHour(pricePerHour);
        }

        // statics

        public static Position Restore(int id, string name, decimal pricePerHour) 
            => new(id, name, pricePerHour);

        public static Position Create(string name, decimal pricePerHour) 
            => new(name, pricePerHour) { };

        // props

        public int Id { get; private set; } = default;
        public string Name { get; private set; }
        public decimal PricePerHour { get; private set; }

        // private setters

        [MemberNotNull(nameof(Name))]
        private void SetName(string value)
        {
            ValidateName(value);

            Name = value;
        }

        [MemberNotNull(nameof(PricePerHour))]
        private void SetPricePerHour(decimal value)
        {
            ValidatePricePerHour(value);

            PricePerHour = value;
        }


        // public setters
        public void ChangeName(string value)
        {
            if (Name == value)
                return;

            SetName(value);
        }

        public void ChangePricePerHour(decimal value)
        {
            if (PricePerHour == value)
                return;

            SetPricePerHour(value);
        }

        // validators

        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 50, nameof(name));
        }

        private void ValidatePricePerHour(decimal pricePerHour)
        {
            DataValidator.Min(pricePerHour, 0, nameof(pricePerHour));
        }
    }
}
