using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Info
{
    public record ShiftCashless
    {
        public readonly decimal Value;

        private ShiftCashless(decimal value) => Value = value;
        
        public static ShiftCashless FromValue(decimal value)
        {
            if (value < 0)
                throw new DataValidationException("Сумма безналичной оплаты смены не может быть меньше 0");

            return new(value);
        }

        public static readonly ShiftCashless Empty = new(0);
    }
}
