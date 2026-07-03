using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Presentation.Api.Filters
{
    public class ExceptionsFilter(ILogger<ExceptionsFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<ExceptionsFilter> _logger = logger;

        public void OnException(ExceptionContext context)
        {
            using var activity = Telemetry.Filter.StartRichActivity();

            var ex = context.Exception;

            if (ex is null)
                return;


            if (ex is Shared.Types.Exceptions.ApplicationException exception)
            {
                _logger.LogError("{Type}: {Message}", ex.GetType().ToString().Split(".").Last(), exception.Message);
                context.Result = ResponseEnvelope.FromError(exception).ToErroredObjectResult();
                return;
            }

            context.Result = ResponseEnvelope.InternalServerError.ToErroredObjectResult();
            _logger.LogError("{Type}: {Message}\n{Full}", ex.GetType().ToString().Split(".").Last(), ex.Message, ex.ToString());
        }
    }
}
