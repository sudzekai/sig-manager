namespace Domain.Models.InfoShifts
{
    public partial class InfoShift
    {
        private InfoShift(int shiftId, decimal? cash, decimal? cashLess, string? receiptPhotoFileName)
        {
            ShiftId = shiftId;
            _cash = cash;
            _cashLess = cashLess;
            _receiptPhotoFileName = receiptPhotoFileName;

            _initialized = true;
        }

        private InfoShift(decimal cash, decimal cashLess, string receiptPhotoFileName)
        {
            Cash = cash;
            CashLess = cashLess;
            ReceiptPhotoFileName = receiptPhotoFileName;

            _initialized = true;
        }

        public static InfoShift Restore(int shiftId, decimal? cash, decimal? cashLess, string? receiptPhotoFileName)
            => new(shiftId, cash, cashLess, receiptPhotoFileName);

        public static InfoShift Create(decimal cash, decimal cashLess, string receiptPhotoFileName)
            => new(cash, cashLess, receiptPhotoFileName);

        
        private readonly bool _initialized = false;
    }
}
