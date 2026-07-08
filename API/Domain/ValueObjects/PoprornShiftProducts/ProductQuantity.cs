using Domain.Exceptions;

namespace Domain.ValueObjects.PoprornShiftProducts
{
    public record ProductQuantity
    {
        public readonly int Value;

        private ProductQuantity(int value) => Value = value;

        public static ProductQuantity FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Количество товара не может быть меньше 1");

            return new(value);
        }
    }
}
