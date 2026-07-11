using Domain.Exceptions;
using Shared.App;

namespace Domain.ValueObjects.Users
{
    public record UserPhoneNumber
    {
        public readonly string Value;
        public readonly string LastFour;


        private UserPhoneNumber(string value)
        {
            Value = value;
            LastFour = value[^4..];
        } 

        public static UserPhoneNumber FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Номер телефона не может быть пустым");

            if (value.Length != 12)
                throw new DataValidationException("Номер телефона должен состоять из 12 символов");

            if (!value.StartsWith("+79"))
                throw new DataValidationException("Номер телефона должен начинаться с +79");

            if (!value.Replace("+79", "").All(char.IsDigit))
                throw new DataValidationException("Номер телефона должен состоять из цифр");

            return new(value);
        }
    }
}
