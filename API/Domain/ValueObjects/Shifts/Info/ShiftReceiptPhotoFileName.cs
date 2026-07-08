using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts.Info
{
    public record ShiftReceiptPhotoFileName
    {
        public readonly string Value;

        private ShiftReceiptPhotoFileName(string value) => Value = value;

        public static ShiftReceiptPhotoFileName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Название файла фотографии сверки итогов не может быть пустым");

            if (value.Length > 255)
                throw new DataValidationException("Название файла фотографии сверки итогов не может быть длиннее 255 символов");

            return new(value);
        }
    }
}
