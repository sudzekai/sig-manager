using Domain.Exceptions;

namespace Domain.Tools
{
    internal class DataValidator
    {
        public static void NullOrWhiteSpace(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new DataValidationException($"{fieldName} не может быть пустым");
        }

        public static void MaxLength(string value, int maxLength, string fieldName)
        {
            if (value.Length > maxLength)
                throw new DataValidationException($"{fieldName} не может быть длиннее {maxLength} символов");
        }

        public static void MinLength(string value, int minLength, string fieldName)
        {
            if (value.Length < minLength)
                throw new DataValidationException($"{fieldName} не может быть короче {minLength} символов");
        }

        public static void LengthEquals(string value, int length, string fieldName)
        {
            if (value.Length != length)
                throw new DataValidationException($"{fieldName} должен быть длиной {length} символов");
        }

        public static void Min(int value, int min, string fieldName)
        {
            if (value < min)
                throw new DataValidationException($"{fieldName} должен быть больше {min-1}");
        }

        public static void Max(int value, int max, string fieldName)
        {
            if (value > max)
                throw new DataValidationException($"{fieldName} должен быть меньше {max+1}");
        }

        public static void OneOf(string value, List<string> enums, string fieldName)
        {
            if (!enums.Any(x => x.Equals(value, StringComparison.OrdinalIgnoreCase)))
                throw new DataValidationException($"{fieldName} должен быть одним из {string.Join(", ", enums)}");
        }
    }
}
