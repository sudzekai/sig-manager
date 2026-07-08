using Domain.Exceptions;

namespace Domain.ValueObjects.Positions
{
    public record PositionId
    {
        public readonly int Value;

        private PositionId(int value) => Value = value;

        public static PositionId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор должности не может быть меньше 1");

            return new(value);
        }
    }
}
