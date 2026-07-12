using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Info;

namespace Domain.Models.InfoShifts
{
    public partial class InfoShift
    {
        public ShiftId ShiftId
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

        public ShiftCash? Cash
        {
            get;
            private set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ShiftCashless? Cashless
        {
            get;
            private set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ShiftReceiptPhotoFileName? ReceiptPhotoFileName
        {
            get;
            private set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public decimal? Total
        {
            get
            {
                if (Cash is not null && Cashless is not null)
                    return Cash.Value + Cashless.Value;

                return null;
            }
        }
    }
}
