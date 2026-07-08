using Domain.Exceptions;

namespace Domain.ValueObjects.PushSubscriptions
{
    public record SubscriptionId
    {
        public readonly int Value;

        private SubscriptionId(int value) => Value = value;

        public static SubscriptionId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор подписки не может быть меньше 1");

            return new(value);
        }
    }
}
