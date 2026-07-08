using Domain.Models.Base;
using Domain.ValueObjects.Positions;

namespace Domain.Models.Positions
{
    public partial class Position : DomainModelBase
    {
        private Position(PositionId id, PositionName name, PositionPricePerHour pricePerHour)
        {
            Id = id;
            Name = name;
            PricePerHour = pricePerHour;

            _initialized = true;
        }

        private Position(PositionName name, PositionPricePerHour pricePerHour)
        {
            Name = name;
            PricePerHour = pricePerHour;

            _initialized = true;
        }

        public static Position Restore(PositionId id, PositionName name, PositionPricePerHour pricePerHour)
            => new(id, name, pricePerHour);

        public static Position Create(PositionName name, PositionPricePerHour pricePerHour)
            => new(name, pricePerHour);
    }
}
