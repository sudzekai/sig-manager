using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Ticket
{
    public record ShiftTicketPrice
    {
        public readonly decimal Value;

        private ShiftTicketPrice(decimal value) => Value = value;

        public static ShiftTicketPrice FromValue(decimal value)
        {
            if (value < 200)
                throw new DataValidationException("Стоимость билета смены не может быть меньше 200");

            return new(value);
        }
    }
}
