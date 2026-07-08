using Domain.Exceptions;

namespace Domain.ValueObjects.Rights
{
    public record RightId
    {
        public readonly int Value;

        private RightId(int value) => Value = value;

        public static RightId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор права не может быть меньше 1");

            return new(value);
        }
    }
}
