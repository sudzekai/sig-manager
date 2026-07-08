using Domain.Exceptions;

namespace Domain.ValueObjects.Cars
{
    public record CarName
    {
        public readonly string Value;

        private CarName(string value) => Value = value;

        public static CarName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Название машины не может быть пустым");

            if (value.Length > 50)
                throw new DataValidationException("Название машины не может быть длиннее 50 символов");

            return new(value);
        }
    }
}
