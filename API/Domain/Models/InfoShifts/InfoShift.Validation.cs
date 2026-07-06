using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models.InfoShifts
{
    public partial class InfoShift
    {
        public void ValidateCash([NotNull] decimal? cash)
        {
            DataValidator.Null(cash, nameof(cash));
            DataValidator.Min(cash.Value, 0, nameof(cash));
        }

        public void ValidateCashLess([NotNull] decimal? cashLess)
        {
            DataValidator.Null(cashLess, nameof(cashLess));
            DataValidator.Min(cashLess.Value, 0, nameof(cashLess));
        }

        public void ValidateReceiptPhotoFileName([NotNull] string? receiptPhotoFileName)
        {
            DataValidator.NullOrWhiteSpace(receiptPhotoFileName, nameof(receiptPhotoFileName));
            DataValidator.MaxLength(receiptPhotoFileName, 255, nameof(receiptPhotoFileName));
        }
    }
}
