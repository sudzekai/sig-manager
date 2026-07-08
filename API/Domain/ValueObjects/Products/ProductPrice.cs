using Domain.Exceptions;

namespace Domain.ValueObjects.Products
{
    public record ProductPrice
    {
        public readonly decimal Value;

        private ProductPrice(decimal value) => Value = value;

        public static ProductPrice FromValue(decimal value)
        {
            if (value < 1)
                throw new DataValidationException("Стоимость товара не может быть меньше 1");

            return new(value);
        }
    }
}
