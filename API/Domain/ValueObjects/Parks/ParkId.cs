using Domain.Exceptions;

namespace Domain.ValueObjects.Parks
{
    public record ParkId
    {
        public readonly int Value;

        private ParkId(int value) => Value = value;

        public static ParkId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор парка не может быть меньше 1");

            return new(value);
        }
    }
}
