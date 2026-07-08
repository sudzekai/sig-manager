using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts
{
    public record ShiftStatus
    {
        public readonly string Value;

        private ShiftStatus(string value) => Value = value;

        public static ShiftStatus FromValue(string value)
            => value?.ToLower() switch
            {
                "opened" => Opened,
                "closed" => Closed,
                _ => throw new DataValidationException("Статус смены должен быть одним из: opened, closed")
            };

        public static readonly ShiftStatus Opened = new("opened");

        public static readonly ShiftStatus Closed = new("closed");
    }
}
