using Domain.Exceptions;

namespace Domain.ValueObjects.Parks
{
    public record ParkName
    {
        public readonly string Value;

        private ParkName(string value) => Value = value;

        public static ParkName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Название парка не может быть пустым");

            if (value.Length > 25)
                throw new DataValidationException("Название парка не может быть длиннее 25 символов");

            return new(value);
        }
    }
}
