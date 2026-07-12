using Domain.Exceptions;

namespace Domain.ValueObjects.Users
{
    public record UserVerificationCode
    {
        public readonly string Value;

        private UserVerificationCode(string value) => Value = value;

        public static UserVerificationCode FromValue(string value)
        {
            if (value == Empty.Value)
                return Empty;

            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Код подтверждения не может быть пустым");

            if (value.Length != 6)
                throw new DataValidationException("Код подтверждения должен состоять из 6 символов");

            return new(value);
        }

        public static readonly UserVerificationCode Empty = new("empty");
    }
}
