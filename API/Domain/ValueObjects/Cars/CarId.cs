using Domain.Exceptions;

namespace Domain.ValueObjects.Cars
{
    public record CarId
    {
        public readonly int Value;

        private CarId(int value) => Value = value;

        public static CarId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор машины не может быть меньше 1");

            return new(value);
        }
    }
}
