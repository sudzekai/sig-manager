using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Info;
using System.ComponentModel;

namespace Domain.Models.InfoShifts
{
    public partial class InfoShift : INotifyPropertyChanged
    {
        public ShiftId? ShiftId
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

        public ShiftCashLess? CashLess
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
                if (Cash is not null && CashLess is not null)
                    return Cash.Value + CashLess.Value;

                return null;
            }
        }
    }
}
