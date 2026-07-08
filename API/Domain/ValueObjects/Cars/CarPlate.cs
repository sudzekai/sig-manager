using Domain.Exceptions;

namespace Domain.ValueObjects.Cars
{
    public record CarPlate
    {
        public readonly string Value;

        private CarPlate(string value) => Value = value;

        public static CarPlate FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Идентификатор контроллера машины не может быть пустым");

            if (value.Length > 150)
                throw new DataValidationException("Идентификатор контроллера машины не может быть длиннее 150 символов");

            return new(value);
        }
    }
}
