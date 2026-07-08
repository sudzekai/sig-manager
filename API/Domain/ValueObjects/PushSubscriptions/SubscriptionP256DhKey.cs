using Domain.Exceptions;

namespace Domain.ValueObjects.PushSubscriptions
{
    public record SubscriptionP256DhKey
    {
        public readonly string Value;

        private SubscriptionP256DhKey(string value) => Value = value;

        public static SubscriptionP256DhKey FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Ключ подписки не может быть пустым");

            return new(value);
        }
    }
}
