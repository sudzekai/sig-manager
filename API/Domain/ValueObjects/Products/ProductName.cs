using Domain.Exceptions;

namespace Domain.ValueObjects.Products
{
    public record ProductName
    {
        public readonly string Value;

        private ProductName(string value) => Value = value;

        public static ProductName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Название товара не может быть пустым");

            if (value.Length > 50)
                throw new DataValidationException("Название товара не может быть длиннее 50 символов");

            return new(value);
        }
    }
}
