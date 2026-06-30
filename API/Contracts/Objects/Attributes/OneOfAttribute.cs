using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class OneOfAttribute : ValidationAttribute
    {
        private readonly List<string> _enums;
        public bool Required { get; set; } = true;

        public OneOfAttribute(List<string> enums)
        {
            _enums = enums;
        }

        public OneOfAttribute(string enums)
        {
            _enums = [.. enums.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())];
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string str)
                return ValidationResult.Success;

            if (!_enums.Any(x => x.Equals(str, StringComparison.OrdinalIgnoreCase)))
            {
                if (string.IsNullOrWhiteSpace(str) && !Required)
                    return ValidationResult.Success;

                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
