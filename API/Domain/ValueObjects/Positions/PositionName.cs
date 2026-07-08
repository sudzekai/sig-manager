using Domain.Exceptions;

namespace Domain.ValueObjects.Positions
{
    public record PositionName
    {
        public readonly string Value;

        private PositionName(string value) => Value = value;

        public static PositionName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Название должности не может быть пустым");

            if (value.Length > 25)
                throw new DataValidationException("Название должности не может быть длиннее 25 символов");

            return new(value);
        }
    }
}
