using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Types.Exceptions;

namespace Presentation.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = string.Join(", ",
                    context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                throw new BadRequestException(errors);
            }
        }
    }
}
