using Domain.Exceptions;

namespace Domain.ValueObjects.Products
{
    public record ProductId
    {
        public readonly int Value;

        private ProductId(int value) => Value = value;

        public static ProductId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор товара не может быть меньше 1");

            return new(value);
        }
    }
}
