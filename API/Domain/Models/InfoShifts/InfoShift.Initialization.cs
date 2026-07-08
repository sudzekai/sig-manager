using Domain.Models.Base;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Info;

namespace Domain.Models.InfoShifts
{
    public partial class InfoShift : DomainModelBase
    {
        private InfoShift(ShiftId shiftId, ShiftCash? cash, ShiftCashLess? cashLess, ShiftReceiptPhotoFileName? receiptPhotoFileName)
        {
            ShiftId = shiftId;
            Cash = cash;
            CashLess = cashLess;
            ReceiptPhotoFileName = receiptPhotoFileName;

            _initialized = true;
        }

        private InfoShift(ShiftCash cash, ShiftCashLess cashLess, ShiftReceiptPhotoFileName receiptPhotoFileName)
        {
            Cash = cash;
            CashLess = cashLess;
            ReceiptPhotoFileName = receiptPhotoFileName;

            _initialized = true;
        }

        public static InfoShift Restore(ShiftId shiftId, ShiftCash? cash, ShiftCashLess? cashLess, ShiftReceiptPhotoFileName? receiptPhotoFileName)
            => new(shiftId, cash, cashLess, receiptPhotoFileName);

        public static InfoShift Create(ShiftCash cash, ShiftCashLess cashLess, ShiftReceiptPhotoFileName receiptPhotoFileName)
            => new(cash, cashLess, receiptPhotoFileName);
    }
}
