using Domain.Exceptions;

namespace Domain.ValueObjects.Users
{
    public record UserId
    {
        public readonly int Value;

        private UserId(int value) => Value = value;

        public static UserId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор пользователя не может быть меньше 1");

            return new(value);
        }
    }
}
