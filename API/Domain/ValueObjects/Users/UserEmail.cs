using Domain.Exceptions;

namespace Domain.ValueObjects.Users
{
    public record UserEmail
    {
        public readonly string Value;

        private UserEmail(string value) => Value = value;

        public static UserEmail FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Электронная почта не может быть пустой");

            if (value.Length > 255)
                throw new DataValidationException("Электронная почта не может быть длиннее 255 символов");

            return new(value);
        }
    }
}
