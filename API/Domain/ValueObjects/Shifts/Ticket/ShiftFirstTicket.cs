using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Ticket
{
    public record class ShiftFirstTicket
    {
        public readonly int Value;

        private ShiftFirstTicket(int value) => Value = value;

        public static ShiftFirstTicket FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Номер первого билета смены не может быть меньше 1");

            return new(value);
        }
    }
}
