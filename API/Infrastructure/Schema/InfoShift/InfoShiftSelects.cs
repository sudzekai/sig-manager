namespace Infrastructure.Schema.InfoShift
{
    public class InfoShiftSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            InfoShiftSchema.ShiftId,
            InfoShiftSchema.Cash,
            InfoShiftSchema.Cashless,
            InfoShiftSchema.ReceiptPhotoFileName,

        ];

        public static readonly IReadOnlyList<string> Full = [
            InfoShiftSchema.Cash,
            InfoShiftSchema.Cashless,
            InfoShiftSchema.ReceiptPhotoFileName,
        ];
    }
}
