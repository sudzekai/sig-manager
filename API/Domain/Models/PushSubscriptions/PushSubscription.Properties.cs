using Domain.ValueObjects.PushSubscriptions;
using Domain.ValueObjects.Users;

namespace Domain.Models.PushSubscriptions
{
    public partial class PushSubscription
    {
        public SubscriptionId? Id
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public UserId UserId
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public SubscriptionEndpoint Endpoint
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public SubscriptionP256DhKey P256DhKey
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public SubscriptionAuthKey AuthKey
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }
    }
}
