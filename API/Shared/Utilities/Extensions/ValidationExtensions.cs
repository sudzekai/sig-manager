using Shared.Types.Exceptions;
using Shared.Utilities.Validation;

namespace Shared.Utilities.Extensions
{
    public static class ValidationExtensions
    {
        public static CustomValidationObject ThrowIfNull(this CustomValidationObject obj) => obj ?? throw new BadRequestException("Тело запроса обязательно");
    }
}
