using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Info
{
    public record ShiftCash
    {
        public readonly decimal Value;

        private ShiftCash(decimal value) => Value = value;

        public static ShiftCash FromValue(decimal value)
        {
            if (value < 0)
                throw new DataValidationException("Сумма наличной оплаты смены не может быть меньше 0");

            return new(value);
        }

        public static readonly ShiftCash Empty = new(0);
    }
}
