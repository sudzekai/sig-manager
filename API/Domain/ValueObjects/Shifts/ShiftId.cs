using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts
{
    public record ShiftId
    {
        public readonly int Value;

        private ShiftId(int value) => Value = value;

        public static ShiftId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор смены не может быть меньше 1");

            return new(value);
        }
    }
}
