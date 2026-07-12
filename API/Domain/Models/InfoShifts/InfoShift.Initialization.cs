using Domain.Models.Base;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Info;

namespace Domain.Models.InfoShifts
{
    public partial class InfoShift : DomainModelBase
    {
        private InfoShift(ShiftId shiftId, ShiftCash? cash, ShiftCashless? cashLess, ShiftReceiptPhotoFileName? receiptPhotoFileName)
        {
            ShiftId = shiftId;
            Cash = cash;
            Cashless = cashLess;
            ReceiptPhotoFileName = receiptPhotoFileName;

            _initialized = true;
        }

        public static InfoShift Restore(ShiftId shiftId, ShiftCash? cash, ShiftCashless? cashLess, ShiftReceiptPhotoFileName? receiptPhotoFileName)
            => new(shiftId, cash, cashLess, receiptPhotoFileName);

        public static InfoShift Create(ShiftId shiftId, ShiftCash cash, ShiftCashless cashLess, ShiftReceiptPhotoFileName receiptPhotoFileName)
            => new(shiftId, cash, cashLess, receiptPhotoFileName);
    }
}
