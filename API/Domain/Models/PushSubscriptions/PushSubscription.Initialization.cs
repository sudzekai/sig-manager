using Domain.Models.Base;
using Domain.ValueObjects.PushSubscriptions;
using Domain.ValueObjects.Users;

namespace Domain.Models.PushSubscriptions
{
    public partial class PushSubscription : DomainModelBase
    {
        private PushSubscription(SubscriptionId? id, UserId userId, SubscriptionEndpoint endpoint, SubscriptionP256DhKey p256DhKey, SubscriptionAuthKey authKey)
        {
            Id = id;
            UserId = userId;
            Endpoint = endpoint;
            P256DhKey = p256DhKey;
            AuthKey = authKey;

            _initialized = true;
        }

        private PushSubscription(UserId userId, SubscriptionEndpoint endpoint, SubscriptionP256DhKey p256DhKey, SubscriptionAuthKey authKey)
        {
            UserId = userId;
            Endpoint = endpoint;
            P256DhKey = p256DhKey;
            AuthKey = authKey;

            _initialized = true;
        }

        public static PushSubscription Restore(SubscriptionId id, UserId userId, SubscriptionEndpoint endpoint, SubscriptionP256DhKey p256DhKey, SubscriptionAuthKey authKey)
            => new(id, userId, endpoint, p256DhKey, authKey);

        public static PushSubscription Create(UserId userId, SubscriptionEndpoint endpoint, SubscriptionP256DhKey p256DhKey, SubscriptionAuthKey authKey)
            => new(userId, endpoint, p256DhKey, authKey);
    }
}
