using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contracts.Objects.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal partial class PhoneNumberAttribute : ValidationAttribute
    {
        [GeneratedRegex(@"^\+79\d{9}$")]
        private static partial Regex PhonePattern();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string phoneNumber)
                return ValidationResult.Success;

            if (!PhonePattern().IsMatch(phoneNumber))
                return new ValidationResult($"Некорректный формат номера телефона");

            return ValidationResult.Success;
        }

    }
}
