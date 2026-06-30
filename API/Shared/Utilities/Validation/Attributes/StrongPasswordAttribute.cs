using System.ComponentModel.DataAnnotations;

namespace Shared.Utilities.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class StrongPasswordAttribute(int minLength = 8, int maxLength = 25) : ValidationAttribute
    {
        public int MinLength { get; set; } = minLength;
        public int MaxLength { get; set; } = maxLength;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string str)
                return ValidationResult.Success;

            if (value is not string password)
                return ValidationResult.Success;

            if (password.Length < MinLength)
                return new ValidationResult( $"Пароль должен содержать минимум {MinLength} символов.");

            if (password.Length > MaxLength)
                return new ValidationResult( $"Пароль должен содержать максимум {MaxLength} символов.");

            if (!password.Any(char.IsUpper))
                return new ValidationResult("Пароль должен содержать хотя бы одну заглавную букву.");

            if (!password.Any(char.IsLower))
                return new ValidationResult("Пароль должен содержать хотя бы одну строчную букву.");

            if (!password.Any(char.IsDigit))
                return new ValidationResult("Пароль должен содержать хотя бы одну цифру.");

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                return new ValidationResult("Пароль должен содержать хотя бы один специальный символ.");

            return ValidationResult.Success;
        }
    }
}
