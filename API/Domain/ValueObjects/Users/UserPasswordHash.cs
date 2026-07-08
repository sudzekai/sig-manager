using Domain.Exceptions;

namespace Domain.ValueObjects.Users
{
    public record UserPasswordHash
    {
        public readonly string Value;

        private UserPasswordHash(string value) => Value = value;

        public static UserPasswordHash FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Пароль не может быть пустым");

            return new(value);
        }
    }
}
