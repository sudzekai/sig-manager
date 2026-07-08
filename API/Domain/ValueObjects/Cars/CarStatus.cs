using Domain.Exceptions;

namespace Domain.ValueObjects.Cars
{
    public record CarStatus
    {
        public readonly string Value;

        private CarStatus(string value) => Value = value;

        public static CarStatus FromValue(string value)
            => value?.ToLower() switch
            {
                "working" => Working,
                "broken" => Broken,
                _ => throw new DataValidationException("Статус машины должен быть одним из: working, broken")
            };

        public static readonly CarStatus Working = new("working");

        public static readonly CarStatus Broken = new("broken");
    }
}
