using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Ticket
{
    public record ShiftLastTicket
    {
        public readonly int Value;

        private ShiftLastTicket(int value) => Value = value;

        public static ShiftLastTicket FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Номер последнего билета смены не может быть меньше 1");
            
            return new(value);
        }

        public static ShiftLastTicket FromValue(int value, ShiftFirstTicket firstTicket)
        {
            if (value < firstTicket.Value)
                throw new DataValidationException("Номер последнего билета смены не может быть меньше номера первого билета");

            return FromValue(value);
        }
    }
}
