using Domain.Exceptions;

namespace Domain.ValueObjects.Users
{
    public record UserFullName
    {
        public readonly string Value;

        private UserFullName(string value) => Value = value;

        public static UserFullName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Полное имя не может быть пустым");

            if (value.Length > 255)
                throw new DataValidationException("Полное имя не может быть длиннее 255 символов");

            if (value.Split(' ').Length != 3)
                throw new DataValidationException("Полное имя должно состоять из трёх слов");

            if (value.Any(char.IsDigit))
                throw new DataValidationException("Полное имя не может содержать цифры");

            return new(value);
        }
    }
}
