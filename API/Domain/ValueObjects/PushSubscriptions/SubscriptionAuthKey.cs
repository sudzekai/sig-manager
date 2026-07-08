using Domain.Exceptions;

namespace Domain.ValueObjects.PushSubscriptions
{
    public record SubscriptionAuthKey
    {
        public readonly string Value;

        private SubscriptionAuthKey(string value) => Value = value;

        public static SubscriptionAuthKey FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Аутентификационный ключ подписки не может быть пустым");

            return new(value);
        }
    }
}
