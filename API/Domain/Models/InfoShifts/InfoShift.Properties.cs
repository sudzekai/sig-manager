using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Models.InfoShifts
{
    public partial class InfoShift : INotifyPropertyChanged
    {
        private int _shiftId;
        private decimal? _cash;
        private decimal? _cashLess;
        private string? _receiptPhotoFileName;

        public int ShiftId
        {
            get => _shiftId; 
            set => _shiftId = value;
        }

        public decimal? Cash
        {
            get => _cash;
            private set
            {
                if (_cash == value)
                    return;

                ValidateCash(value);

                _cash = value;

                OnPropertyChanged();
            }
        }

        public decimal? CashLess
        {
            get => _cashLess;
            private set
            {
                if (_cashLess == value)
                    return;

                ValidateCashLess(value);

                _cashLess = value;

                OnPropertyChanged();
            }
        }

        public string? ReceiptPhotoFileName
        {
            get => _receiptPhotoFileName;
            private set
            {
                if (_receiptPhotoFileName == value)
                    return;

                ValidateReceiptPhotoFileName(value);

                _receiptPhotoFileName = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
