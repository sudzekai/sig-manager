using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Info
{
    public record ShiftCashLess
    {
        public readonly decimal Value;

        private ShiftCashLess(decimal value) => Value = value;

        public static ShiftCashLess FromValue(decimal value)
        {
            if (value < 0)
                throw new DataValidationException("Сумма безналичной оплаты смены не может быть меньше 0");

            return new(value);
        }
    }
}
