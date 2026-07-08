using Domain.Exceptions;
using Domain.ValueObjects.Shifts.Info;

namespace Domain.ValueObjects.Positions
{
    public record PositionPricePerHour
    {
        public readonly decimal Value;

        private PositionPricePerHour(decimal value) => Value = value;

        public static PositionPricePerHour FromValue(decimal value)
        {
            if (value < 0)
                throw new DataValidationException("Почасовая ставка должности не может быть меньше 0");

            return new(value);
        }
    }
}
