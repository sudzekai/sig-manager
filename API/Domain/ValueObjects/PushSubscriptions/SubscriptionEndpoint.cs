using Domain.Exceptions;
using Domain.ValueObjects.Positions;

namespace Domain.ValueObjects.PushSubscriptions
{
    public record SubscriptionEndpoint
    {
        public readonly string Value;

        private SubscriptionEndpoint(string value) => Value = value;

        public static SubscriptionEndpoint FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Эндпоинт подписки не может быть пустым");

            return new(value);
        }
    }
}
