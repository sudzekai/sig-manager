using Domain.Exceptions;

namespace Domain.ValueObjects.Users
{
    public record Username
    {
        public readonly string Value;

        private Username(string value) => Value = value;

        public static Username FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Имя пользователя не может быть пустым");

            if (value.Length > 25)
                throw new DataValidationException("Имя пользователя не может быть длиннее 25 символов");

            return new(value);
        }
    }
}
