using Domain.Exceptions;

namespace Domain.ValueObjects.Rights
{
    public record RightCode
    {
        public readonly string Value;

        private RightCode(string value) => Value = value;

        public static RightCode FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Код права не может быть пустым");

            if (value.Length > 100)
                throw new DataValidationException("Код права не может быть длиннее 100 символов");

            return new(value);
        }
    }
}
