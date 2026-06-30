using Shared.Types.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Shared.Utilities.Validation
{
    public abstract class CustomValidationObject
    {
        public List<string> ValidationErrors { get; private set; } = [];

        public bool IsValid => ValidationErrors.Count == 0;

        public CustomValidationObject Validate()
        {
            ICollection<ValidationResult> errors = [];
            Validator.TryValidateObject(this, new(this), errors);

            ValidationErrors = [.. errors.Select(e => e.ErrorMessage ?? "").Where(e => !string.IsNullOrWhiteSpace(e))];

            return this;
        }

        public CustomValidationObject ThrowIfInvalid() => _ = IsValid ? this : throw new BadRequestException(string.Join(", ", ValidationErrors));
    }
}
